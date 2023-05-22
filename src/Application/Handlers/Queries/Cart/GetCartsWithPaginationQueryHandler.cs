using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contract.Common.Models;
using Application.Contract.Queries;
using Application.Contract.Services;
using AutoMapper;
using MediatR;


namespace EShop.Application.Handlers;
public class GetCartsWithPaginationQueryHandler : IRequestHandler<GetCartWithPaginationQuery, PaginatedList<CartDto>>
{
    private readonly ICartService _cartService;

    public GetCartsWithPaginationQueryHandler(ICartService cartService)
    {
        _cartService = cartService;
    }

    public async Task<PaginatedList<CartDto>> Handle(GetCartWithPaginationQuery request, CancellationToken cancellationToken)
    {

        return await _cartService.GetCarts(request);

   
    }
}
