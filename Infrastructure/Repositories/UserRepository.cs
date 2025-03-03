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
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserEntity>> GetAllUsersWithProducts(CancellationToken cancellationToken)
        {
            return await _context.Users.Include(x => x.Products).ToListAsync(cancellationToken) ?? [];
        }

        public async Task<UserEntity?> GetUserWithProducts(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Users.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
