using System;
using Management.Domain.Documents;

namespace Management.Domain.Commands.ShiftCommands
{
    public class CreateShiftCommand : CommandWithIdResponse
    {
       
        public DateTime ShiftStart { get; }
        public DateTime ShiftEnd { get; }

        public Guid EmployeeId { get; set; }
        //public float Duration { get; set; }

        public CreateShiftCommand(DateTime shiftStart, DateTime shiftEnd , Guid EmployeeId)
        {
            //Duration = duration;
            ShiftStart = shiftStart;
            ShiftEnd = shiftEnd;
            this.EmployeeId = EmployeeId;
        }
    }
}