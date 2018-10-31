using System.Threading;
using System.Threading.Tasks;
using Management.Domain.Queries;
using Management.Persistence.Model;
using SimpleSoft.Mediator;

namespace Management.Domain.QueryHandler
{
    public class UserQueryHandler :
        IQueryHandler<GetUsers, User>
    {
        public UserQueryHandler()
        {
            
        }
        
        public async Task<User> HandleAsync(GetUsers query, CancellationToken ct)
        {
            return null;
        }
    }
}