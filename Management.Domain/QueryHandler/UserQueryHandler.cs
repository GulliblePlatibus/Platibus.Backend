using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Management.Domain.Queries;
using Management.Persistence.Model;
using Management.Persistence.Repositories;
using SimpleSoft.Mediator;

namespace Management.Domain.QueryHandler
{
    public class UserQueryHandler :
        IQueryHandler<GetUsers, IEnumerable<User>>
    {
        private readonly IUserRepository _userRepository;

        public UserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<IEnumerable<User>> HandleAsync(GetUsers query, CancellationToken ct)
        {
          _userRepository.
        }
    }
}