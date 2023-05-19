using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contract.Common.Mappings;
using AutoMapper;
using EShop.Domain;
using EShop.Domain.Enums;

namespace Application.Contract.Queries;


public class ProductDto : IMapFrom<Product>
{
    public int   Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public int Price { get; set; }
    public int Benefit { get; set; }
    public int TotalPrice  { get => Price + Benefit; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductDto>()
            .ForMember(d => d.Type, opt => opt.MapFrom(s => (ProductType)s.Type));
    }

}

