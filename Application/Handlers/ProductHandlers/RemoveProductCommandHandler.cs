using Application.Commands.ProductCommands;
using AutoMapper;
using Azure.Core;
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
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, IResult<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RemoveProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
        }
        public async Task<IResult<bool>> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var isDeleted = await _unitOfWork.ProductRepository.DeleteAsync(request.id, cancellationToken);

            if (isDeleted)
                return Result<bool>.Success(true);

            return Result<bool>.Failure(false, "Failed deleting product");

        }
    }
}
