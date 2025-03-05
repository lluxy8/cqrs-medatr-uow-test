using Core.DTOs;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ProductCommands
{
    public record CreateProductCommand(ProductCreateDto dto) : IRequest<IResult<Guid>>;
}
