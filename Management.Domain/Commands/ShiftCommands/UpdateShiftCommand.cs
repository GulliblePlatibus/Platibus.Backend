using System;
using Management.Documents.Documents;
using SimpleSoft.Mediator;

namespace Management.Domain.Commands.ShiftCommands
{
    public class UpdateShiftCommand : Command<IdResponse>
    {
        public Guid Id { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public Guid Employeeid { get; set; }


        public UpdateShiftCommand(Guid id, DateTime shiftStart, DateTime shiftEnd , Guid Employeeid)
        {
            Id = id;
            ShiftStart = shiftStart;
            ShiftEnd = shiftEnd;
            this.Employeeid = Employeeid;
        }
    }
}
