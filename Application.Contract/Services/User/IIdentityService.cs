using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contract.Commands;
using Application.Contract.Common.Models;
using EShop.Domain;
using MediatR;

namespace Application.Contract.Services;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(int userId);

    Task<bool> IsInRoleAsync(int userId, string role);

    Task<bool> AuthorizeAsync(int userId, string policyName);

    Task<Result> CreateUserAsync(CreateUserCommand user);

    Task<Result> DeleteUserAsync(int userId);
    string GetToken(CreateTokenCommand user);
    int UserId { get; }
}