using Application.Contract.Common.Models;
using Application.Contract.Services;
using EShop.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Contract.Common.Security;
using Application.Contract.Commands;
using MediatR;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace EShop.Application.Services;
public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly IOptionsSnapshot<AppSettings> _settings;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public IdentityService(
        UserManager<User> userManager,
        IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        IOptionsSnapshot<AppSettings> settings,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _settings = settings;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string?> GetUserNameAsync(int userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<Result> CreateUserAsync(CreateUserCommand userCommand)
    {
        var user = new User
        {
            UserName = userCommand.UserName,
            Email = userCommand.UserName,
            FirstName = userCommand.FirstName,
            LastName = userCommand.LastName,
        };


        var result = _userManager.CreateAsync(user, userCommand.Password).Result;
        //if (result.Succeeded)
        //    return user.Id;
        //else
        //    return 0;

        return result.ToApplicationResult();
    }

    public async Task<bool> IsInRoleAsync(int userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(int userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(int userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(User user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public string GetToken(CreateTokenCommand usertoken)
    {
        var user = _userManager.FindByNameAsync(usertoken.UserName).Result;
        if (user != null)
        {
            var role = _userManager.GetRolesAsync(user).Result;

            var result = _userManager.CheckPasswordAsync
                (user, usertoken.Password).Result;

            if (!result)
            {
                throw new Exception(" User Not Found");
            }

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", user.Id.ToString())
                };
            var token = GetToken(authClaims);

            var tk = new JwtSecurityTokenHandler().WriteToken(token);

            return tk;
        }
        throw new Exception(" User Not Found");



    }


    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.SigningKey));

        var token = new JwtSecurityToken(
            issuer: _settings.Value.Issuer,
            audience: _settings.Value.Audience,
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }



    public int UserId => Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("UserId").Value);

}
