﻿using Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserCommands
{
    public record UpdateUserCommand(Guid id, UserCreateDto dto) : IRequest<Guid>;
}
