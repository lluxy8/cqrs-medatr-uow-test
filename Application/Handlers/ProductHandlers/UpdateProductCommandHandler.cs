using Application.Commands.ProductCommands;
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

namespace Application.Handlers.ProductHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, IResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            var existingProduct = await _unitOfWork.ProductRepository.GetByIdAsync(request.id, cancellationToken);
            if (existingProduct is null)
            {
                return Result<Guid>.Failure(Guid.Empty, "Product not found");
            }

            _mapper.Map(request.dto, existingProduct);

            existingProduct.Id = request.id;

            await _unitOfWork.ProductRepository.UpdateAsync(existingProduct, cancellationToken);

            return Result<Guid>.Success(existingProduct.Id);
        }
    }
}
