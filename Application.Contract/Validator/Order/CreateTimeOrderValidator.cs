using Application.Contract.Commands;
using MediatR;
namespace Application.Contract.Validator;

public class CreateTimeOrderValidator : IPipelineBehavior<CreateOrderCommand, int>
{
    public CreateTimeOrderValidator()
    {

    }
    public async Task<int> Handle(CreateOrderCommand request, RequestHandlerDelegate<int> next, CancellationToken cancellationToken)
    {
        if (!(DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 19))
        {
            throw new Exception("ثبت سفارش امکان پذیر نمی باشد");
        }
        else
        {
            var response = await next();
            return response;
        }
    }

}