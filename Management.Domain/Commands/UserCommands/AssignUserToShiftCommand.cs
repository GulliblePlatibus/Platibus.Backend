using System;
using Management.Domain.Documents;

namespace Management.Domain.Commands
{
    public class AssignUserToShiftCommand : CommandWithIdResponse
    {
        public Guid Id { get; }
        public Guid ShiftId { get; }

        public AssignUserToShiftCommand(Guid id, Guid shiftId)
        {
            

            Id = id;
            ShiftId = shiftId;
            
        }
    }
}