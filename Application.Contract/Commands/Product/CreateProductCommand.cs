namespace Application.Contract.Commands;

using EShop.Domain.Enums;
using MediatR;
public record CreateProductCommand : IRequest<int>
{
    public string Name { get; set; }
    public ProductType Type { get; set; }
    public int Price { get; set; }
    public int Benefit { get; set; }
}