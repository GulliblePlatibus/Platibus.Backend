using System;
using Management.Domain.Documents;

namespace Management.Domain.Commands.ShiftCommands
{
    public class CreateShiftCommand : CommandWithIdResponse
    {
       
        public DateTime ShiftStart { get; }
        public DateTime ShiftEnd { get; }

        public CreateShiftCommand(DateTime shiftStart, DateTime shiftEnd)
        {
            ShiftStart = shiftStart;
            ShiftEnd = shiftEnd;
        }
    }
}