using System;
using Management.Domain.Documents;
namespace Management.Domain.Commands
{
	public class CreateUserCommand : CommandWithIdResponse
    {
        public string Name { get; }
		public string Address { get; }
		public int Age { get; }

		public CreateUserCommand(string name, int age, string address)
        {
            Age = age;
			Address = address;
			Name = name;
		}
    }
}
