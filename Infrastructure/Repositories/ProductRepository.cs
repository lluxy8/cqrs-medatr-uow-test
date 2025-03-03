using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductEntity>> GetAllProductsWithUser(CancellationToken cancellationToken)
        {
            return await _context.Products.Include(x => x.User).ToListAsync(cancellationToken) ?? [];
        }

        public async Task<ProductEntity?> GetProductWithUser(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Products.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            
        }
    }
}
