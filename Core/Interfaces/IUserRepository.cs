﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        Task<UserEntity?> GetUserWithProducts(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<UserEntity>> GetAllUsersWithProducts(CancellationToken cancellationToken);
    }
}
