using System.Security.Claims;
using Application.Contract.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();


    public int CurrentUserId
    {
        get
        {
            try
            {
                var userId = ((ClaimsIdentity)HttpContext.User.Identity).Claims.Where(c => c.Type == "UserId")
                    .Select(c => c.Value).SingleOrDefault();
                return Convert.ToInt32(userId);
            }
            catch (Exception ex)
            {
            }
            return 0;
        }
    }
}
