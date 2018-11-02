using System;
using Management.Domain.Documents;
namespace Management.Domain.Commands
{
	public class CreateUserCommand : CommandWithIdResponse
    {
	    public int _acceslevel { get;  }
	    public string Name { get; }
		public string Email { get; }
		public string Password { get; }

		public CreateUserCommand(string name, string email, string password , int acceslevel)
		{
			_acceslevel = acceslevel;
			Name = name;
			Email = email;
			Password = password;
		}
    }
}
