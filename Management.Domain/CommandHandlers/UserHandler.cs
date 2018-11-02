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
	    private readonly IManagerRepository _managerRepository;

	    public UserHandler(IUserRepository userRepository , IManagerRepository managerRepository)
	    {
		    this.userRepository = userRepository;
		    _managerRepository = managerRepository;
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

			var result = await _managerRepository.InsertManager(new Manager()
			{
				Email = cmd.Email,
				Id = id,
				Name = cmd.Name,
				test = cmd.Email
			});

			if (!result.IsSuccessful)
			{
				return new IdResponse(Guid.Empty , false);
			}
			
			return new IdResponse(id);
		}
	}
}
