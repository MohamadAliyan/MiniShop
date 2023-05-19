using Application.Contract.Common.Models;
using Application.Contract.Queries;

namespace Application.Contract.Services;

public interface IOrderService : IService
{
    Task<int> CreateOrder( string address);

    Task<PaginatedList<OrderDto>> GetOrders(GetOrdersWithPaginationQuery request);
}