using Application.Queries.ProductQueries;
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
    public class GetAllProductsWithUserQueryHandler : IRequestHandler<GetAllProductsWithUserQuery, IEnumerable<ProductEntity>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsWithUserQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductEntity>> Handle(GetAllProductsWithUserQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetAllProductsWithUser(cancellationToken);
        }
    }
}
