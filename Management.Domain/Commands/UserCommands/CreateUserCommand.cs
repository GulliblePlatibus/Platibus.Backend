using System;
using Management.Domain.Documents;
namespace Management.Domain.Commands
{
	public class CreateUserCommand : CommandWithIdResponse
    {
	    public Guid Id { get; }
	    public int _acceslevel { get;  }
	    public string Name { get; }
		public string Email { get; }
		public string Password { get; }
	    public double Wage { get; }
	    public DateTime EmploymentDate { get; }

		public CreateUserCommand(Guid id, string name, string email, string password, int acceslevel, double wage, DateTime employmentDate )
		{
			if (id.Equals(Guid.Empty))
			{
				throw new ArgumentException(nameof(id) + " CreateUserCommand may not be initiated with a id value of Guid.Empty");
			}
			
			Id = id;
			_acceslevel = acceslevel;
			Name = name;
			Email = email;
			Password = password;
			Wage = wage;
			EmploymentDate = employmentDate;


		}
    }
}
