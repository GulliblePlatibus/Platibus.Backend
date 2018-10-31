using System.Threading;
using System.Threading.Tasks;
using Management.Documents.Documents;
using Management.Domain.Commands.OrganizationCommands;
using SimpleSoft.Mediator;

namespace Management.Domain.Handlers
{
    public class OrganizationCommandHandler : ICommandHandler<CreateOrganiztion, IdResponse>
    {
        public async Task<IdResponse> HandleAsync(CreateOrganiztion cmd, CancellationToken ct)
        {
            return null;
        }
    }
}