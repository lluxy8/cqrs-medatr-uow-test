﻿using Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ProductCommands
{
    public record UpdateProductCommand(Guid id, ProductCreateDto dto) : IRequest<Guid>;
}
