using System.Collections.Generic;

namespace Management.Domain.DomainModels.Users
{
    public class Manager : User
    {
      public  Department Department { get; set; }
      public List<Team> Teams { get; set; }
        
    }
}