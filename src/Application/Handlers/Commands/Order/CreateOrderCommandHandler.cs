using Application.Contract.Commands;
using Application.Contract.Services;
using MediatR;

namespace EShop.Application.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{

    private readonly IOrderService _orderService;

    public CreateOrderCommandHandler(IOrderService orderService)
    {

        _orderService = orderService;
    }

    public  Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {

        return _orderService.CreateOrder( request.Address);

    }


}