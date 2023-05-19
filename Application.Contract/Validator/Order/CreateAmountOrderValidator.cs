using Application.Contract.Commands;
using Application.Contract.Common.Interfaces;
using Application.Contract.Services;
using MediatR;
namespace Application.Contract.Validator;

public class CreateAmountOrderValidator : IPipelineBehavior<CreateOrderCommand, int>
{
    private readonly ICartService _cartService;
    private readonly ICurrentUserService _currentUserService;
    public CreateAmountOrderValidator(ICartService cartService, ICurrentUserService currentUserService)
    {
        _cartService = cartService;
        _currentUserService = currentUserService;
    }
    public async Task<int> Handle(CreateOrderCommand request, RequestHandlerDelegate<int> next, CancellationToken cancellationToken)
    {

        var currentCart = _cartService.GetCurrentCart(_currentUserService.UserId);
        

        if (!_cartService.ValidateCart(currentCart))
        {
           throw new Exception("   حداقل مبلغ سفارش 50 هزار تومان می باشد");
        }
        else
        {
            var response = await next();
            return response;
        }
    }

}