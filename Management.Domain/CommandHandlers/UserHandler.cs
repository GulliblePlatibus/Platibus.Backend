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
	ICommandHandler<CreateUserCommand, IdResponse> , ICommandHandler<DeleteUserByIdCommand , IdResponse> , ICommandHandler<UpdateUserCommand , IdResponse>
    {
		private readonly IUserRepository _userRepository;
	   

	    public UserHandler(IUserRepository userRepository)
	    {
		    _userRepository = userRepository;
		   
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

			var id = cmd.Id;

			var result = await _userRepository.InsertAsync(new User
			{
				Email = cmd.Email,
				Id = id,
				Name = cmd.Name,
				AccessLevel = cmd._acceslevel,
				BaseWage = cmd.Wage,
				EmploymentDate = cmd.EmploymentDate
				
			});
/*
			if (!)
			{
				return new IdResponse(Guid.Empty , false);
			}
			*/
			return new IdResponse(id);
		}

	    public async Task<IdResponse> HandleAsync(DeleteUserByIdCommand cmd, CancellationToken ct)
	    {
		    if (cmd.Id.Equals(Guid.Empty))
		    {
			    return IdResponse.Unsuccessful("Id is empty");
		    }
		    

		    var user = await _userRepository.GetByIdAsync(cmd.Id);

		    var result = await _userRepository.DeleteByTAsync(user);
		    return IdResponse.Successful(user.Id);
	    }

	    public async Task<IdResponse> HandleAsync(UpdateUserCommand cmd, CancellationToken ct)
	    {
		    if (cmd.Id.Equals(Guid.Empty))
		    {
			    return IdResponse.Unsuccessful("User id is empty");
		    }

		    var user = new User
		    {
			    Name = cmd.Name,
			    Email = cmd.Email,
			    AccessLevel = cmd.Acceslevel,
			    Id = cmd.Id
		    };

		    var result = await _userRepository.UpdateAsync(user);
		    
		    
		    return IdResponse.Successful(user.Id);
	    }
    }
}
