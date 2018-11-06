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
        IQueryHandler<GetUsers, IEnumerable<User>> , IQueryHandler<GetUserById, User>
    {
        public IUserRepository UserRepository { get; }

        public UserQueryHandler(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        
        public async Task<IEnumerable<User>> HandleAsync(GetUsers query, CancellationToken ct)
        {
            var result = await UserRepository.GetAllAsync();

            return result ;
        }

        public Task<User> HandleAsync(GetUserById query, CancellationToken ct)
        {
            
            var result = UserRepository.GetByIdAsync(query.Id);

            return result;
        }
    }
}