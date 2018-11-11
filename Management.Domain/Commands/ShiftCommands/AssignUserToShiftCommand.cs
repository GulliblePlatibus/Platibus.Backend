using System;
using Management.Documents.Documents;
using SimpleSoft.Mediator;

namespace Management.Domain.Commands.ShiftCommands
{
    public class AssignUserToShiftCommand : Command<IdResponse>
    {
        public Guid EmployeeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid ShiftId { get; set; }

        public AssignUserToShiftCommand(Guid employeeId, DateTime startTime, DateTime endTime, Guid shiftId)
        {
            EmployeeId = employeeId;
            EmployeeId = employeeId;
            StartTime = startTime;
            EndTime = endTime;
            ShiftId = shiftId;
        }
    }
}