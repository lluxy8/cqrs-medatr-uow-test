using Application.Queries.UserQueries;
using Core.Entities;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.UserHandlers
{
    public class GetAllUsersWithProductsQueryHandler : IRequestHandler<GetAllUsersWithProductsQuery, IEnumerable<UserEntity>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersWithProductsQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserEntity>> Handle(GetAllUsersWithProductsQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllUsersWithProducts(cancellationToken);
        }
    }

}
