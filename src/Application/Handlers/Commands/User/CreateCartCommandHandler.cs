
using Application.Contract.Commands;
using Application.Contract.Common.Models;
using Application.Contract.Services;
using EShop.Domain;
using MediatR;

namespace EShop.Application.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
{

    private readonly IIdentityService _UserService;

    public CreateUserCommandHandler(IIdentityService UserService)
    {

        _UserService = UserService;

    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {


        return await _UserService.CreateUserAsync(request);

    }


}