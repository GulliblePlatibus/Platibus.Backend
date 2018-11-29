using System;

namespace Management.API.RequestModels
{
    public class UpdateShiftAndEmployeeRequestModel
    {
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public Guid ShiftId { get; set; }
        public Guid EmployeeId { get; set; }

        public UpdateShiftAndEmployeeRequestModel(DateTime shiftStart, DateTime shiftEnd , Guid ShiftId , Guid EmployeeId)
        {
            ShiftStart = shiftStart;
            ShiftEnd = shiftEnd;
            this.ShiftId = ShiftId;
            this.EmployeeId = EmployeeId;
        }
    }
}