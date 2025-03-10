﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<ProductEntity>
    {
        Task<ProductEntity?> GetProductWithUser(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<ProductEntity>> GetAllProductsWithUser(CancellationToken cancellationToken);
    }
}
