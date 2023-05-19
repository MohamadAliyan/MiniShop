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
public class GetOrdersWithPaginationQueryHandler : IRequestHandler<GetOrdersWithPaginationQuery, PaginatedList<OrderDto>>
{
    private readonly IOrderService _orderService;

    public GetOrdersWithPaginationQueryHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<PaginatedList<OrderDto>> Handle(GetOrdersWithPaginationQuery request, CancellationToken cancellationToken)
    {

        return await _orderService.GetOrders(request);

   
    }
}
