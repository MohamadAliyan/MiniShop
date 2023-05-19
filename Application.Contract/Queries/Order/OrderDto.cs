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


public class OrderDto : IMapFrom<Order>
{

 
    
    public int TotalAmount { get; set; }
    public string Address { get; set; }
    public string Status { get; set; }
    public string Date { get; set; }

    public  List<OrderItemDto> OrderItems { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderDto>()
                  .ForMember(d => d.Status, opt => opt.MapFrom(s => (OrderStatus)s.Status))
                  .ForMember(d => d.Date, opt => opt.MapFrom(s => s.Date.GetShortNumericPersianDate()))
                  ;
    
    }

}

public class OrderItemDto : IMapFrom<OrderItem>
{
    public int OrderId { get; set; }
    public string  ProductName { get; set; }
    public int Quantity { get; set; }
    public int Amount { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderItem, OrderItemDto>()
        .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.Name));
    }
}

