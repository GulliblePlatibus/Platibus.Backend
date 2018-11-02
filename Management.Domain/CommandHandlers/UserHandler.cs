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

			switch (cmd._acceslevel)
			{
				case 1:
					// opret medarbejder
					break;
				case 2:
					//opret mellemleder
					break;
				case 3:
					// opret admini
					break;
			}
			
			if (string.IsNullOrEmpty(cmd.Name))
			{
				return  IdResponse.Unsuccessful("cannot create user with an empty name");
				
			}
			
			var id = Guid.NewGuid();

			var result = await userRepository.InsertAsync(new User()
			{
				Email = cmd.Email,
				Id = id,
				Name = cmd.Name,
				AccessLevel = cmd._acceslevel,
				
			});
/*
			if (!)
			{
				return new IdResponse(Guid.Empty , false);
			}
			*/
			return new IdResponse(id);
		}
	}
}
