using System.Collections.Generic;
using Management.Domain.DomainModels.Schedule;

namespace Management.Domain.DomainModels.Users
{
    public class Employee : User
    {
        public double Wage { get; set; }
        public Department Department { get; set; }
        public List<Shift> Shifts { get; set; }
        public List<Skill> Skills { get; set; }
        public EmployeeInfo EmployeeInfo { get; set; }

        public Employee(double wage, Department department, EmployeeInfo employeeInfo)
        {
            Wage = wage;
            Department = department;
            EmployeeInfo = employeeInfo;
        }

        /// <summary>
        /// Takes an unlimited number of shifts and adds them to the employees shifts
        /// </summary>
        /// <param name="shifts"></param>
        public void AssignShifts(params Shift[] shifts)
        {
            foreach (var shift in shifts)
            {
                if (!Shifts.Contains(shift))
                {
                  Shifts.Add(shift);  
                }
            }
        }
    }
}