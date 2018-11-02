using System;
using Dapper.FluentMap.Dommel.Mapping;
using Management.Persistence.Documents;
namespace Management.Persistence.Model
{

	public class UserMap : DommelEntityMap<User>
	{
		
		public UserMap()
		{
			ToTable("users");
			Map(x => x.Id).ToColumn("id").IsKey();
			Map(x => x.Email).ToColumn("email");
			Map(x => x.Name).ToColumn("name");
			Map(x => x.AccessLevel).ToColumn("accesslevel");
			
		}
	}
	
	public class User : IEntity
    {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
	    public int AccessLevel { get; set; }
	    
		
    }
}
