using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contract.Common.Models;
using Application.Contract.Queries;
using Application.Contract.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EShop.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Contract.Common.Mappings;
namespace EShop.Application.Services;

public class ProductService : Service, IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public Task<int> AddProduct(Product product)
    {
        _productRepository.Insert(product);
        return _productRepository.SaveChanges();

    }

  public async Task<PaginatedList<ProductDto>> GetProducts(GetProductsWithPaginationQuery request)
    {
        var res= await _productRepository.GetAll()
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        return res;

    }
}
