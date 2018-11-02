using System;
using Dapper.FluentMap.Dommel.Mapping;
using Management.Persistence.Documents;
namespace Management.Persistence.Model
{
	public class MappingUser : DommelEntityMap<User>
	{
		public MappingUser()
		{
			ToTable("users");
			Map(x => x.Id).IsKey();
		}
	}
	
	public class User : IEntity
    {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
    }
}
