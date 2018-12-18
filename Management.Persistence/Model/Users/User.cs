using System;
using Dapper.FluentMap.Dommel.Mapping;
using Management.Persistence.Documents;
namespace Management.Persistence.Model
{


	public enum UserRoles
{
	Admin,
	Administrative,
	Manager,
	Employee,
	Unknown
}

	public class UserMap : DommelEntityMap<User>
	{
		
		public UserMap()
		{
			ToTable("users");
			Map(x => x.Id).ToColumn("id").IsKey();
			Map(x => x.Email).ToColumn("email");
			Map(x => x.Name).ToColumn("name");
			Map(x => x.AccessLevel).ToColumn("accesslevel");
			Map(x => x.BaseWage).ToColumn("basewage");
			Map(x => x.EmploymentDate).ToColumn("employmentDate");

		}
	}
	
	public class User : IEntity
    {
		
		public string Name { get; set; }
		public string Email { get; set; }
	    public UserRoles AccessLevel { get; set; }
	    public Guid Id { get; set; }
	    public double BaseWage { get; set; }
	    public DateTime EmploymentDate { get; set; }
		
    }
}
