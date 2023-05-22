using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contract.Common.Models;
using MediatR;

namespace Application.Contract.Queries;
public record GetCartWithPaginationQuery : IRequest<PaginatedList<CartDto>>
{
    
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
