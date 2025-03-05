using Application.Commands.ProductCommands;
using Application.Commands.UserCommands;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.UserHandlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var existingUser = await _unitOfWork.UserRepository.GetByIdAsync(request.id, cancellationToken);
                if (existingUser is null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Guid.Empty;
                }  

                var user = _mapper.Map(request.dto, existingUser);
                user.Id = request.id;

                await _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

                await _unitOfWork.CommitTransactionAsync();

                return user.Id;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
