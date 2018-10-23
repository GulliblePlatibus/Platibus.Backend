using System;
using SimpleSoft.Mediator;
using Management.Queries.Queries;
using Management.Persistence.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Management.Persistence.Repositories;

namespace Management.Queries.Handlers
{
	public class UserQueryHandler :
	IQueryHandler<GetUserQuery, User>
    {
		private readonly IUserRepository userRepository;

		public UserQueryHandler(IUserRepository userRepository)
        {
			this.userRepository = userRepository;
        }

		public async Task<User> HandleAsync(GetUserQuery query, CancellationToken ct)
		{
			//Read a user from database!!!
			return await userRepository.GetById(query.UserId);
		}
	}
}
