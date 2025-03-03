using Application.Queries.UserQueries;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.UserHandlers
{
    public class GetUserByIdWithProductsQueryHandler : IRequestHandler<GetUserByIdWithProductsQuery, UserWithProductsViewModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdWithProductsQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserWithProductsViewModel?> Handle(GetUserByIdWithProductsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserWithProducts(request.Id, cancellationToken);
            return _mapper.Map<UserWithProductsViewModel>(user);
        }
    }
}
