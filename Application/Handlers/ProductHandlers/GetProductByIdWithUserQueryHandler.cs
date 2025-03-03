using Application.Queries.ProductQueries;
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
    public class GetProductByIdWithUserQueryHandler : IRequestHandler<GetProductByIdWithUserQuery, ProductViewModelWithUser>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdWithUserQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductViewModelWithUser?> Handle(GetProductByIdWithUserQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductWithUser(request.Id, cancellationToken);
            return _mapper.Map<ProductViewModelWithUser>(product);
        }
    }
}
