using System;
using System.Threading;
using System.Threading.Tasks;
using Management.Documents.Documents;
using Management.Domain.Commands;
using Management.Persistence.Model;
using Management.Persistence.Repositories;
using SimpleSoft.Mediator;

namespace Management.Domain.Handlers
{
    public class WorkScheduleHandler : ICommandHandler<AssignUserToShiftCommand , IdResponse>
    {

        private readonly IWorkScheduleRepository _workScheduleRepository;

        public WorkScheduleHandler(IWorkScheduleRepository workScheduleRepository)
        {
            _workScheduleRepository = workScheduleRepository;
        }
        
        public async Task<IdResponse> HandleAsync(AssignUserToShiftCommand cmd, CancellationToken ct)
        {
            if (cmd.Id.Equals(Guid.Empty) || cmd.ShiftId.Equals(Guid.Empty))
            {
                return IdResponse.Unsuccessful("Employee Id or shift Id is empty");
            }

            var assignedUser = new WorkSchedule
            {
                Id = cmd.Id,
                ShiftId = cmd.ShiftId
            };

            var result = await _workScheduleRepository.InsertAsync(assignedUser);
            
            return new IdResponse(assignedUser.Id);

        }
    }
}