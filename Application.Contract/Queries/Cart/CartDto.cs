using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contract.Common;
using Application.Contract.Common.Mappings;
using AutoMapper;
using EShop.Domain;
using EShop.Domain.Common;
using EShop.Domain.Enums;


namespace Application.Contract.Queries;


public class CartDto : IMapFrom<Cart>
{
    public int UserId { get; set; }
    public int DiscountPercent { get; set; }
    public int DiscountAmount { get; set; }
    public int Amount { get; set; }
    public int TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public string Date { get; set; }
    public bool IsActive { get; set; }
    public virtual List<CartItemDto> CartItems { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Cart, CartDto>()
                  
                  .ForMember(d => d.Date, opt => opt.MapFrom(s => s.Date.GetShortNumericPersianDate()))
                  ;
    }
}

public class CartItemDto : IMapFrom<CartItem>
{
    public int CartId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public int Amount { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CartItem, CartItemDto>()
        .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.Name));
    }
}

