using System;

namespace Management.API.RequestModels
{
    public class AddUserToShiftRequestModel
    {
        public Guid id { get; set; }
        public Guid EmployeeOnShift { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        
    }
}