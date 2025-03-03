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
    public class GetAllProductsWithUserQueryHandler : IRequestHandler<GetAllProductsWithUserQuery, IEnumerable<ProductViewModelWithUser>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsWithUserQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductViewModelWithUser>> Handle(GetAllProductsWithUserQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProductsWithUser(cancellationToken);
            return _mapper.Map<IEnumerable<ProductViewModelWithUser>>(products);

        }
    }
}
