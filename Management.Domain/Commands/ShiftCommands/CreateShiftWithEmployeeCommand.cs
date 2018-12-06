using System;
using Management.Documents.Documents;
using SimpleSoft.Mediator;

namespace Management.Domain.Commands.ShiftCommands
{
    public class CreateShiftWithEmployeeCommand : Command<IdResponse>
    {
        public DateTime StartDate { get; set; }
        public DateTime Enddate { get; set; }
        public Guid EmployeeId { get; set; }

        public CreateShiftWithEmployeeCommand(DateTime StartDate , DateTime Enddate , Guid employeeId)
        {
            this.StartDate = StartDate;
            this.Enddate = Enddate;
            this.EmployeeId = employeeId;
        }
    }
}