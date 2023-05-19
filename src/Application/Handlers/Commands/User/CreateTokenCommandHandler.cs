
using Application.Contract.Commands;
using Application.Contract.Common.Models;
using Application.Contract.Services;
using EShop.Domain;
using MediatR;

namespace EShop.Application.Handlers.Commands.User;

public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, string>
{

    private readonly IIdentityService _identityService;

    public CreateTokenCommandHandler(IIdentityService identityService)
    {

        _identityService = identityService;

    }

    public async Task<string> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {


        return _identityService.GetToken(request);

    }


}