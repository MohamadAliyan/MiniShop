using Application.Contract.Commands;
using Application.Contract.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;



public class OrderController : ApiControllerBase
{

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<int>> Create([FromBody] CreateOrderCommand request)
    {
        
        var result = await Mediator.Send(request);
        return Created("", result);
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> GetAll([FromQuery] GetOrdersWithPaginationQuery request)
    {

        var result = await Mediator.Send(request);
        return Ok(result);
    }


}
