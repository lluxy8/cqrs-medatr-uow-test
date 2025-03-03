using Core.Entities;
using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.ProductQueries
{
    public record GetAllProductsWithUserQuery() : IRequest<IEnumerable<ProductViewModelWithUser>>;
}
