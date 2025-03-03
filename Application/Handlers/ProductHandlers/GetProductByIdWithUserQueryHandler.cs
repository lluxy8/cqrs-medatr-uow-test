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
    public class GetProductByIdWithUserQueryHandler : IRequestHandler<GetProductByIdWithUserQuery, ProductEntity>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdWithUserQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductEntity?> Handle(GetProductByIdWithUserQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetProductWithUser(request.Id, cancellationToken);
        }
    }
}
