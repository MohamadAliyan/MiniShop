﻿
using Application.Contract.Commands;
using Application.Contract.Common.Models;
using Application.Contract.Queries;
using EShop.Domain;

namespace Application.Contract.Services;

public interface ICartService : IService
{
    int CalculateDiscount(int amount, int discountAmount, int discountPercent);
    List<CartItem> CalculateCartItems(List<CreateCartItemCommand> cartItemsCommand);
    bool ValidateCart(Cart cart);
    Cart GetCurrentCart(int userId);
    Task<int> AddToCart(CreateCartCommand createCartCommand);
    Task<PaginatedList<CartDto>> GetCarts(GetCartWithPaginationQuery request);
}