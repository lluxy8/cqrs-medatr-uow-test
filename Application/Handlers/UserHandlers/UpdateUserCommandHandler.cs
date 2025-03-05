using Application.Commands.ProductCommands;
using Application.Commands.UserCommands;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.UserHandlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, IResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult<Guid>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.UserRepository.GetByIdAsync(request.id, cancellationToken);
            if (existingUser is null)
                return Result<Guid>.Failure(Guid.Empty, "User not found");

            var user = _mapper.Map(request.dto, existingUser);
            user.Id = request.id;

            await _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

            return Result<Guid>.Success(user.Id);
        }
    }
}
