using System;
using System.Threading;
using System.Threading.Tasks;
using Management.Documents.Documents;
using Management.Domain.Commands;
using Management.Persistence.Model;
using Management.Persistence.Model.Budget;
using Management.Persistence.Repositories;
using SimpleSoft.Mediator;

namespace Management.Domain.Handlers
{
    public class AssignSupplementsHandler : ICommandHandler<AssignSupplementToUserCommand, IdResponse>
    {
        private readonly IAssignSuppRepository _assignSuppRepository;

        public AssignSupplementsHandler(IAssignSuppRepository assignSuppRepository)
        {
            _assignSuppRepository = assignSuppRepository;
        }

        public async Task<IdResponse> HandleAsync(AssignSupplementToUserCommand cmd, CancellationToken ct)
        {
            if (cmd.UserId.Equals(Guid.Empty) || cmd.SupplementId.Equals(Guid.Empty))
            {
                return IdResponse.Unsuccessful("Either user id or supplement id is empty");
            }

            var assignedsupplement = new AssignedSupplements
            {
                Id = cmd.UserId,
                SuppId = cmd.SupplementId,

            };

            var result = await _assignSuppRepository.InsertAsync(assignedsupplement);

            return new IdResponse(assignedsupplement.Id);
        }
        
        
    }
}