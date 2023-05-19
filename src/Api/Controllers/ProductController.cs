using Application.Contract.Commands;
using Application.Contract.Common.Security;
using Application.Contract.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


public class ProductController : ApiControllerBase
{

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<int>> Create([FromBody] CreateProductCommand request)
    {

        var result = await Mediator.Send(request);
        return Created("", result);
    }

   
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> GetAll([FromQuery] GetProductsWithPaginationQuery request)
    {

        var result = await Mediator.Send(request);
        return Ok( result);
    }


    
}
