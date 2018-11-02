using System;
using SimpleSoft.Mediator;
using Management.Domain.Commands;
using System.Threading;
using System.Threading.Tasks;
using EmailValidation;
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

			if (string.IsNullOrEmpty(cmd.Name))
			{
				return new IdResponse(Guid.Empty , false );
			}
			
			var id = Guid.NewGuid();
			
			var result = await userRepository.InsertUser(new User
			{
				Id = "testhansi1234",
				Email = cmd.Email,
				Name = cmd.Name,
				Password = BCrypt.Net.BCrypt.HashPassword(cmd.Password)
			});

			if (!result.IsSuccessful)
			{
				return new IdResponse(Guid.Empty , false);
			}
			
			return new IdResponse(id);
		}
	}
}
