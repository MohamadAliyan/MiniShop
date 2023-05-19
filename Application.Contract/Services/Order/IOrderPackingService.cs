using Application.Contract.Services;
using EShop.Domain.Enums;

namespace EShop.Application.Contract.Service;

public interface IOrderPackingService : IService
{
   Task<int> CreateOrderPacking( int orderId);
   int CalculateCost(ProductType productType, int totalAmountOrder);

}