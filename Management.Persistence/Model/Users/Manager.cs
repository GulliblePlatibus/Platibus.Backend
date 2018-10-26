using System.Collections.Generic;

namespace Management.Persistence.Model
{
    public class Manager : User
    {
      public  Department Department { get; set; }
      public List<Team> Teams { get; set; }
        
    }
}