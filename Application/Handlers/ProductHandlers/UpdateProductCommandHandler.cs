using Application.Commands.ProductCommands;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.ProductHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var existingProduct = await _unitOfWork.ProductRepository.GetByIdAsync(request.id, cancellationToken);
                if (existingProduct is null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return Guid.Empty;
                }

                _mapper.Map(request.dto, existingProduct);

                existingProduct.Id = request.id;

                await _unitOfWork.ProductRepository.UpdateAsync(existingProduct, cancellationToken);

                await _unitOfWork.CommitTransactionAsync();

                return existingProduct.Id;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
