using Application.Contract.Common.Interfaces;
using EShop.Application.Contract.Service;
using EShop.Domain;
using EShop.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Services;

public class OrderPackingService : Service, IOrderPackingService
{
    private readonly IOrderPackingRepository _OrderPackingRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly ICartRepository _cartRepository;
    private readonly ICurrentUserService _currentUserService;

    public OrderPackingService(IOrderPackingRepository OrderPackingRepository, IOrderRepository orderRepository, ICartRepository cartRepository, ICurrentUserService currentUserService)
    {
        _OrderPackingRepository = OrderPackingRepository;
        _orderRepository = orderRepository;
        _cartRepository = cartRepository;
        _currentUserService = currentUserService;
    }

    public int CalculateCost(ProductType productType, int totalAmountOrder)
    {
        var cost = 0;
        cost = Convert.ToInt32(productType == ProductType.Normal ? Constant.PostCost : Constant.ExpressPostCost) + totalAmountOrder;
        return cost;

    }

    public Task< int> CreateOrderPacking( int orderId)
    {
        var currentOrder = _orderRepository.GetBy(p => p.UserId == _currentUserService.UserId
                  && p.Id == orderId
                  && p.Status == OrderStatus.Created)
               .Include(p => p.OrderItems)
               .ThenInclude(p => p.Product)
               .SingleOrDefault();
        var orderpackingItem = new List<OrderPackingItem>();
        var groupedOrderItems = currentOrder.OrderItems.GroupBy(p => new { p.Product.Type, p.OrderId })
      .Where(group => group.Count() >= 1)
      .ToList();

        foreach (var item in groupedOrderItems)
        {

            foreach (var citem in item)
            {
                orderpackingItem.Add(new OrderPackingItem
                {
                    OrderItemId = citem.Id,
                });
            }
            var orderpacking = new OrderPacking
            {
                OrderId = orderId,
                Cost = item.Key.Type == ProductType.Normal ? Constant.PostCost : Constant.ExpressPostCost,
                //TotalAmount = CalculateCost(item.Key.Type,item.Sum(p=>(p.Amount*p.Quantity))),
                TotalAmount = CalculateCost(item.Key.Type, currentOrder.TotalAmount / groupedOrderItems.Count),
                ShippingMethodType = item.Key.Type == ProductType.Normal ? ShippingMethodType.Post : ShippingMethodType.ExpressPost,
                OrderPackingItems = orderpackingItem
            };
            _OrderPackingRepository.Insert(orderpacking);
            currentOrder.Status = OrderStatus.Completed;
            var cart = _cartRepository.GetBy(p => p.Id == currentOrder.CartId).Single();
            cart.Status = OrderStatus.Completed;
            cart.IsActive = false;
            _orderRepository.Update(currentOrder);
            _cartRepository.Update(cart);


        }
        return _OrderPackingRepository.SaveChanges();

    }
}
