using EShop.Domain.Enums;
using MediatR;

namespace Application.Contract.Commands;

public record CreateCartCommand : IRequest<int>
{
    //public int UserId { get; set; }
    public OrderStatus Status { get; set; }
    public int DiscountPercent { get; set; }
    public int DiscountAmount { get; set; }

    public List<CreateCartItemCommand> CreateCartItemCommand { get; set; }

}
public record CreateCartItemCommand : IRequest<int>
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }

}