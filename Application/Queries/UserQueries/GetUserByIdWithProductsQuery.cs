using Core.Entities;
using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.UserQueries
{
    public record GetUserByIdWithProductsQuery(Guid Id) : IRequest<UserWithProductsViewModel>;
}
