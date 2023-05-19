using Application.Contract.Common.Models;
using Application.Contract.Queries;
using EShop.Domain;

namespace Application.Contract.Services;

public interface IProductService: IService
{
    Task<int> AddProduct(Product product);
    Task<PaginatedList<ProductDto>> GetProducts(GetProductsWithPaginationQuery request);
}