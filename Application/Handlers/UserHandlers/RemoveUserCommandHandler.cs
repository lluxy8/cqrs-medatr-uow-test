using Application.Commands.ProductCommands;
using Application.Commands.UserCommands;
using AutoMapper;
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
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, IResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RemoveUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult<bool>> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {

            var isDeleted = await _unitOfWork.UserRepository.DeleteAsync(request.id, cancellationToken);

            if (isDeleted)
                return Result<bool>.Success(true);

            return Result<bool>.Failure(false, "Failed deleting user");
        }
    }
}
