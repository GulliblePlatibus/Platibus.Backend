using System;

namespace Management.Persistence.Model
{
    public class AllShiftsWithEmployees
    {
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid Id { get; set; }
        public Guid EmployeeOnShift { get; set; }
    }
}