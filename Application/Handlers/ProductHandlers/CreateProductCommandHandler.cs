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
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.ProductHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, IResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<ProductEntity>(request.dto);

            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.dto.UserId, cancellationToken);

            if (user is null)
            {
                return Result<Guid>.Failure(Guid.Empty, "User not found");
            }

            product.User = user;

            await _unitOfWork.ProductRepository.AddAsync(product, cancellationToken);
            return Result<Guid>.Success(product.Id);
        }
    }
}
