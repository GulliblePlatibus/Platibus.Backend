using System;
using Management.Acquaintance;
using Management.Persistence.Documents;

namespace Management.Domain.DomainModels.Users
{
	public class User : IEntity, IUser
    {
		public string Id { get; set; }

		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
    }
}
