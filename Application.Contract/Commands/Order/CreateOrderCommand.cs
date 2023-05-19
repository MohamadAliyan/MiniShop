using MediatR;

namespace Application.Contract.Commands;

public record CreateOrderCommand : IRequest<int>
{
    //public int UserId { get; set; }
    public string    Address { get; set; }


}