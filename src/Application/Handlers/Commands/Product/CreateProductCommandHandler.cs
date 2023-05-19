using Application.Contract.Commands;
using Application.Contract.Services;
using EShop.Domain;
using MediatR;

namespace EShop.Application.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{

    private readonly IProductService _productService;

    public CreateProductCommandHandler(IProductService productService)
    {

        _productService = productService;
    }

    public  Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Product()
        {
            Name = request.Name,
            Type = request.Type,
            Price = request.Price,
            Benefit = request.Benefit,

        };
        return _productService.AddProduct(entity);

 
    }
}