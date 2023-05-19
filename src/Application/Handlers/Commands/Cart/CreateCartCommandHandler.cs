
using Application.Contract.Commands;
using Application.Contract.Services;
using MediatR;

namespace EShop.Application.Handlers;

public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, int>
{

    private readonly ICartService _cartService;

    public CreateCartCommandHandler(ICartService cartService)
    {

        _cartService = cartService;

    }

    public  Task<int> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        return   _cartService.AddToCart(request);

    }


}