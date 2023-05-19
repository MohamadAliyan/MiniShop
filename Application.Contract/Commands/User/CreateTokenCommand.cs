using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contract.Common.Models;
using EShop.Domain.Enums;
using MediatR;

namespace Application.Contract.Commands;
public record CreateTokenCommand : IRequest<string>
{

    public string UserName { get; set; }
    public string Password { get; set; }


}
