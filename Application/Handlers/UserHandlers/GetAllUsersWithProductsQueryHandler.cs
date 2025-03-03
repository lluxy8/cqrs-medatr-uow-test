using Application.Queries.UserQueries;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Models;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.UserHandlers
{
    public class GetAllUsersWithProductsQueryHandler : IRequestHandler<GetAllUsersWithProductsQuery, IEnumerable<UserWithProductsViewModel>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllUsersWithProductsQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<UserWithProductsViewModel>> Handle(GetAllUsersWithProductsQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsersWithProducts(cancellationToken);
            return _mapper.Map<IEnumerable<UserWithProductsViewModel>>(users);
        }
    }

}
