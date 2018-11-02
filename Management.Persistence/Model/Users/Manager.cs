using System.Collections.Generic;
using Dapper.FluentMap.Dommel.Mapping;

namespace Management.Persistence.Model
{
    public class ManagerMap : DommelEntityMap<Manager>
    {
        public ManagerMap()
        {
            ToTable("manager");
            Map(x => x.Id).ToColumn("id").IsKey();
            Map(x => x.Name).ToColumn("name");
            Map(x => x.test).ToColumn("blabla");
            Map(x => x.Department).Ignore();
            Map(x => x.Teams).Ignore();
        }
    }
    public class Manager : User
    {
      public  Department Department { get; set; }
      public List<Team> Teams { get; set; }
      public string test { get; set; }
        
    }
}