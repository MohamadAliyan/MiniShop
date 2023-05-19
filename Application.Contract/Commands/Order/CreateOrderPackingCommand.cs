
using MediatR;

namespace Application.Contract.Commands;

public record CreateOrderPackingCommand : IRequest<int>
{
    //public int UserId { get; set; }
    public int OrderId { get; set; }

}