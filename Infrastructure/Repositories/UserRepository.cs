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

        public async Task<IEnumerable<UserEntity>> GetAllUsersWithProducts()
        {
            return await _context.Users.Include(x => x.Products).ToListAsync() ?? [];
        }

        public async Task<UserEntity?> GetUserWithProducts(Guid id)
        {
            return await _context.Users.Include(x => x.Products).FirstAsync(x => x.Id == id);
        }
    }
}
