using System;
using SimpleSoft.Mediator;
using Management.Domain.Commands;
using System.Threading;
using System.Threading.Tasks;
using Management.Documents.Documents;
using Management.Persistence.Repositories;
using Management.Persistence.Model;

namespace Management.Domain.Handlers
{
	public class UserHandler : 
	ICommandHandler<CreateUserCommand, IdResponse>
    {
		private readonly IUserRepository userRepository;

		public UserHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
		}

		public async Task<IdResponse> HandleAsync(CreateUserCommand cmd, CancellationToken ct)
		{
			//Do some logics, save the result in the persistence and return response indicating the succes state of the 

			var userId = Guid.NewGuid();
			var result = await userRepository.InsertUser(new User
			{
				Id = userId,
				Name = cmd.Name,
				Address = cmd.Address,
				Age = cmd.Age
			});         

			if(!result.IsSuccessful)
			{
				return new IdResponse(Guid.Empty, false);
			}

			return new IdResponse(userId);
		}
	}
}
