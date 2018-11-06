using System;
using System.Threading;
using System.Threading.Tasks;
using Management.Documents.Documents;
using Management.Domain.Commands.ShiftCommands;
using Management.Persistence.Model;
using Management.Persistence.Repositories;
using SimpleSoft.Mediator;

namespace Management.Domain.Handlers
{
    public class ShiftHandler : 
    ICommandHandler<CreateShiftCommand, IdResponse>
    {
        private readonly IShiftRepository shiftRepository;
        
        public async Task<IdResponse> HandleAsync(CreateShiftCommand cmd, CancellationToken ct)
        {
            if (cmd.ShiftStart > cmd.ShiftEnd)
            {
                return IdResponse.Unsuccessful("cannot create a shift with an end time before the start time");
            }

            var id = Guid.NewGuid();

            var result = await shiftRepository.InsertAsync(new Shift()
            {
                Id = id,
                ShiftStart = cmd.ShiftStart,
                ShiftEnd = new DateTime(2018,11,5,11,55,00)
               
            });
            
            return new IdResponse(id);
        }
    }
}