using System;
using Management.Documents.Documents;
using Management.Domain.Documents;
using SimpleSoft.Mediator;

namespace Management.Domain.Commands
{
    public class AssignSupplementToUserCommand : CommandWithIdResponse
    {
        public Guid UserId { get; }
        public Guid SupplementId { get; }

        public AssignSupplementToUserCommand(Guid userId, Guid supplementId)
        {
            if (userId.Equals(Guid.Empty) || supplementId.Equals(Guid.Empty))
            {
                throw new ArgumentException(nameof(userId));
            }

            UserId = userId;
            SupplementId = supplementId;
            
        }
    }
}