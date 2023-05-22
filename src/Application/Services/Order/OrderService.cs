using Application.Contract.Common.Interfaces;
using Application.Contract.Queries;
using Application.Contract.Services;
using EShop.Domain;
using EShop.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.Contract.Common.Mappings;
using Application.Contract.Common.Models;

namespace EShop.Application.Services;

public class OrderService : Service, IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICartService _cartService;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    public OrderService(IOrderRepository orderRepository,

        ICartService cartService,
        IOrderItemRepository orderItemRepository,
        ICurrentUserService currentUserService,
        IMapper mapper)
    {
        _orderRepository = orderRepository;

        _cartService = cartService;
        _orderItemRepository = orderItemRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public Task<int> CreateOrder( string address)
    {

        var currentCart = _cartService.GetCurrentCart(_currentUserService.UserId);

        var currentOrder = _orderRepository.GetBy(p => p.UserId == _currentUserService.UserId
            && p.CartId == currentCart.Id
            && p.Status == OrderStatus.Created)
         .Include(p => p.OrderItems)
         .SingleOrDefault();
        if (currentOrder != null)
        {
            foreach (var item in currentOrder.OrderItems)
            {

                _orderItemRepository.Delete(item.Id);
            }
            _orderRepository.Delete(currentOrder.Id);
            _orderRepository.SaveChanges();

            var orderItems = new List<OrderItem>();
            foreach (var item in currentCart.CartItems)
            {
                orderItems.Add(new OrderItem
                {
                    Amount = item.Amount,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                });
            }

            var order = new Order
            {
                Address = address,
                Amount = currentCart.Amount,
                CartId = currentCart.Id,
                Status = OrderStatus.Created,
                Date = DateTime.Now,
                DiscountAmount = currentCart.DiscountAmount,
                DiscountPercent = currentCart.DiscountPercent,
                TotalAmount = currentCart.TotalAmount,
                UserId = _currentUserService.UserId,
                OrderItems = orderItems
            };
            _orderRepository.Insert(order);
            return _orderRepository.SaveChanges();
        }

        else
        {
            var orderItems = new List<OrderItem>();
            foreach (var item in currentCart.CartItems)
            {
                orderItems.Add(new OrderItem
                {
                    Amount = item.Amount,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                });
            }

            var order = new Order
            {
                Address = address,
                Amount = currentCart.Amount,
                CartId = currentCart.Id,
                Status = OrderStatus.Created,
                Date = DateTime.Now,
                DiscountAmount = currentCart.DiscountAmount,
                DiscountPercent = currentCart.DiscountPercent,
                TotalAmount = currentCart.TotalAmount,
                UserId = _currentUserService.UserId,
                OrderItems = orderItems
            };
            _orderRepository.Insert(order);
            return _orderRepository.SaveChanges();
        }


    }

    public Task<PaginatedList<OrderDto>> GetOrders(GetOrdersWithPaginationQuery request)
    {
        var currentOrder = _orderRepository.GetBy(p => p.UserId == _currentUserService.UserId)
    .                   Include(p => p.OrderItems).ThenInclude(p=>p.Product)
                         .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                        .PaginatedListAsync(request.PageNumber, request.PageSize);

        return currentOrder;




    }
}
