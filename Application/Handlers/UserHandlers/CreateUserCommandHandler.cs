using Application.Commands.UserCommands;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Models;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.UserHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<UserEntity>(request.dto);

            await _unitOfWork.UserRepository.AddAsync(user, cancellationToken);

            return Result<Guid>.Success(user.Id);
        }
    }
}
