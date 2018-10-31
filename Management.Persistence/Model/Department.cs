using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Management.Persistence.Model
{
    public class Department
    {
        public Collection<Employee> Employees { get; set; }
        public Collection<Manager> Managers { get; set; }
        public List<WorkSchedule> WorkSchedules { get; set; } //Not sure if this should be a list and have a set();
        public List<Budget> Budgets { get; set; }
        
        
    }
}