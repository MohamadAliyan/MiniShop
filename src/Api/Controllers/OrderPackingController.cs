using Application.Contract.Commands;

using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;



public class OrderPackingController : ApiControllerBase
{

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<int>> Create([FromBody] CreateOrderPackingCommand request)
    {
        
        var result = await Mediator.Send(request);
        return Created("", result);
    }

   
    
}
