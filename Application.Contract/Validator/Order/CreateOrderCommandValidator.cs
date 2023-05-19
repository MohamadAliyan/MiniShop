using Application.Contract.Commands;
using FluentValidation;

namespace Application.Contract.Validator.Order;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        
        RuleFor(p=>p.Address).NotEmpty();
    }
    

}

