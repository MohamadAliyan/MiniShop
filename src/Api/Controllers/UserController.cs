using Application.Contract.Commands;
using EShop.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;




public class UserController : ApiControllerBase
{

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [AllowAnonymous]
    public async Task<ActionResult<int>> Create([FromBody] CreateUserCommand request)
    {

        var result = await Mediator.Send(request);
        return Created("", result);
    }



    [HttpPost]
    [Route("GetToken")]
   [AllowAnonymous]
    
    public async Task<ActionResult<int>> GetToken([FromBody] CreateTokenCommand request)
    {

        var result = await Mediator.Send(request);
        return Ok(result);
    }



}
