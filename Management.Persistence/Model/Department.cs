using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Management.Persistence.Model
{
    public class Department
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public Collection<Employee> Employees { get; set; }
        public Manager mananger { get; set; }
        
        
    }
}