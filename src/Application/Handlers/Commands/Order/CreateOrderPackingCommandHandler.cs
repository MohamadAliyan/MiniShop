using Application.Contract.Commands;
using EShop.Application.Contract.Service;
using MediatR;

namespace EShop.Application.Handlers;

public class CreateOrderPackingCommandHandler : IRequestHandler<CreateOrderPackingCommand, int>
{

    private readonly IOrderPackingService _orderPackingService;

    public CreateOrderPackingCommandHandler(IOrderPackingService orderPackingService)
    {

        _orderPackingService = orderPackingService;



    }

    public  Task<int> Handle(CreateOrderPackingCommand request, CancellationToken cancellationToken)
    {
        return _orderPackingService.CreateOrderPacking( request.OrderId);

    }


}