using System;

namespace Management.API.RequestModels
{
    public class UpdateShiftRequestModel
    {
        public Guid ShiftId { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public Guid EmployeeId { get; set; }
    }
}