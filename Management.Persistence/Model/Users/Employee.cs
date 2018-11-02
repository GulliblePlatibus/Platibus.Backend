using System;
using System.Collections.Generic;
using System.IO.Compression;
using Dapper.FluentMap.Dommel.Mapping;

namespace Management.Persistence.Model
{

    public class EmployeeMap : DommelEntityMap<Employee>
    {
        public EmployeeMap()
        {
            
            
            ToTable("employee1");
            Map(x => x.Id).ToColumn("id").IsKey();

            Map(x => x.Name).ToColumn("name");
            Map(x => x.Email).ToColumn("email");
        }
        
    }
    public class Employee : User
    {
        
       // public double Wage { get; set; }
       // public Department Department { get; set; }
       // public List<Shift> Shifts { get; set; }
       // public List<Skill> Skills { get; set; }
       // public EmployeeInfo EmployeeInfo { get; set; }

        public Employee() 
        {

        }

      
    }
}