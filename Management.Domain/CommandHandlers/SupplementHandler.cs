using System;
using System.Threading;
using System.Threading.Tasks;
using Management.Documents.Documents;
using Management.Domain.Commands.SupplementCommands;
using Management.Persistence.Model.Budget;
using Management.Persistence.Repositories;
using SimpleSoft.Mediator;

namespace Management.Domain.Handlers
{
    public class SupplementHandler : ICommandHandler<CreateSupplementCommand, IdResponse>
    {
        
        private readonly ISupplementRepository _supplementRepository;

        public SupplementHandler(ISupplementRepository supplementRepository)
        {
            _supplementRepository = supplementRepository;
        }

        public async Task<IdResponse> HandleAsync(CreateSupplementCommand cmd, CancellationToken ct)
        {
            var id = Guid.NewGuid();

            var result = await _supplementRepository.InsertAsync(new SupplementInfo
            {
                Id = id,
                Decription = cmd.Decription,
                Name = cmd.Name,
                Supplement = cmd.Supplement,
                SupplementDays = cmd.SupplementDays,
                TimeRange = cmd.TimeRange

            });
            
            return new IdResponse(id);
        }
    }
}