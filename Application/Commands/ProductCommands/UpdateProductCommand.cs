using Core.DTOs;
using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ProductCommands
{
    public record UpdateProductCommand(Guid id, ProductViewModel dto) : IRequest<Guid>;
}
