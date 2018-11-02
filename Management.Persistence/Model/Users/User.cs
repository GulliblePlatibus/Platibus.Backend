using System;
using Dapper.FluentMap.Dommel.Mapping;
using Management.Persistence.Documents;
namespace Management.Persistence.Model
{
/*
	public class UserMap : DommelEntityMap<User>
	{
		public UserMap()
		{
			Map(x => x.Id).Ignore();
			Map(x => x.Email).ToColumn("blabla");
		}
	}
	
	*/
	public class User : IEntity
    {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		
    }
}
