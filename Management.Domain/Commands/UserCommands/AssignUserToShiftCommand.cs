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
            if (id.Equals(Guid.Empty))
            {
                throw new ArgumentException(nameof(id) + " CreateUserCommand may not be initiated with a id value of Guid.Empty");
            }

            Id = id;
            ShiftId = shiftId;
            
        }
    }
}