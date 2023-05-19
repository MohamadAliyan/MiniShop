
using Application.Contract.Commands;
using Application.Contract.Common.Interfaces;
using Application.Contract.Services;
using EShop.Domain;
using EShop.Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.Application.Services;

public class CartService : Service, ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICurrentUserService _currentUserService;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository
, ICurrentUserService currentUserService
        
        )
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _currentUserService = currentUserService;
        
    }

    //public int CalculateDiscount(int amount, int discountAmount, int discountPercent)
    //{

    //   var total = amount - discountAmount;
    //  var  totalamount = total * discountPercent / 100;
    //    return totalamount;

    //}


    public int CalculateDiscount(int amount, int discountAmount, int discountPercent)
    {
        double totalamount = 0;
        totalamount = amount - discountAmount;
        totalamount = totalamount - (amount * discountPercent / 100);
        return Convert.ToInt32( totalamount);

    }

    public List<CartItem> CalculateCartItems(List<CreateCartItemCommand> createCartItemCommand)
    {
        var cartItems = new List<CartItem>();
        var allProductIds = createCartItemCommand.Select(x => x.ProductId).ToList();
        var allProducts = _productRepository.GetBy(p => allProductIds.Contains(p.Id)).ToList();
        foreach (var item in createCartItemCommand)
        {
            var product = allProducts.Single(p => p.Id == item.ProductId);

            cartItems.Add(new CartItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Amount = (product.Price + product.Benefit) * item.Quantity

            });
        }
        return cartItems;

    }

    public bool ValidateCart(Cart cart)
    {

        if (cart.TotalAmount > 50000)
        {
            return true;
        }
        else
        {
            return false;


        }
    }

    public Cart? GetCurrentCart(int userId)
    {
        var currentCart = _cartRepository.GetBy(p => p.UserId == userId && p.IsActive)
            .Include(p => p.CartItems)
        .SingleOrDefault();
        if (currentCart != null)
        {
            return currentCart;
        }
        else
            return null;
    }

    public Task<int> AddToCart(CreateCartCommand createCartCommand)
    {
     
       // var cart = GetCurrentCart(createCartCommand.UserId);
        var cart = GetCurrentCart(_currentUserService.UserId);
        if (cart != null)
        {
            var allProductIds = createCartCommand.CreateCartItemCommand.Select(x => x.ProductId).ToList();
            var allProducts = _productRepository.GetBy(p => allProductIds.Contains(p.Id)).ToList();
            foreach (var product in allProducts)
            {
                var item = cart.CartItems.SingleOrDefault(p => p.ProductId == product.Id);
                var found = createCartCommand.CreateCartItemCommand.Single(x => x.ProductId == product.Id);
                if (item != null)
                {

                    item.Quantity += found.Quantity;
                    item.Amount += (product.Price + product.Benefit) * (found.Quantity + item.Quantity);
                    cart.CartItems.Remove(item);
                    cart.CartItems.Add(item);
                }
                else
                {
                    cart.CartItems.Add(new CartItem
                    {
                        ProductId = found.ProductId,
                        Quantity = found.Quantity,
                        Amount = (product.Price + product.Benefit) * found.Quantity

                    });
                }


            }
            _cartRepository.Update(cart);
            return _cartRepository.SaveChanges();

        }
        else
        {
            var cartItems = CalculateCartItems(createCartCommand.CreateCartItemCommand);
            var entity = new Cart()
            {
                UserId = _currentUserService.UserId,
                Status = createCartCommand.Status,
                Amount = cartItems.Sum(p => p.Amount),
                DiscountAmount = createCartCommand.DiscountAmount,
                DiscountPercent = createCartCommand.DiscountPercent,
                Date = DateTime.Now,
                IsActive = true,
                TotalAmount = CalculateDiscount(cartItems.Sum(p => p.Amount), createCartCommand.DiscountAmount, createCartCommand.DiscountPercent),
                CartItems = cartItems
            };
            _cartRepository.Insert(entity);
            return _cartRepository.SaveChanges();
        }

    }
}
