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
public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PaginatedList<ProductDto>>
{
    private readonly IProductService _productService;

    public GetProductsWithPaginationQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<PaginatedList<ProductDto>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
    {

        return await _productService.GetProducts(request);

   
    }
}
