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
    public class GetUserByIdWithProductsQueryHandler : IRequestHandler<GetUserByIdWithProductsQuery, UserEntity>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdWithProductsQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserEntity?> Handle(GetUserByIdWithProductsQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserWithProducts(request.Id, cancellationToken);
        }
    }
}
