﻿using Application.Contract.Commands;
using MediatR;
namespace Application.Contract.Validator;

public class CreateTimeOrderValidator : IPipelineBehavior<CreateOrderCommand, int>
{
    public CreateTimeOrderValidator()
    {

    }
    public async Task<int> Handle(CreateOrderCommand request, RequestHandlerDelegate<int> next, CancellationToken cancellationToken)
    {

        var min = new TimeOnly(8, 0);
        var max = new TimeOnly(19, 0);
        var currentTime = TimeOnly.FromDateTime(DateTime.Now);

        if (!currentTime.IsBetween(min, max))
        {
            throw new Exception("ثبت سفارش امکان پذیر نمی باشد");
        }
        else
        {
            var response = await next();
            return response;

        }

        //if (!(DateTime.Now.Hour >= 8 && DateTime.Now.Hour <= 19))
        //{
        //    throw new Exception("ثبت سفارش امکان پذیر نمی باشد");
        //}
        //else
        //{
        //    var response = await next();
        //    return response;
        //}
    }

}