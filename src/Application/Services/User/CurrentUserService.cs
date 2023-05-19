using System.Security.Claims;
using Application.Contract.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EShop.Application.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    
    public int UserId =>Convert.ToInt32( _httpContextAccessor.HttpContext.User.FindFirst("UserId").Value);
}
