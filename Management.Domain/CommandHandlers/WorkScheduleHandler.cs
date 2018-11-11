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
        private readonly IShiftRepository _shiftRepository;
        

        private readonly IWorkScheduleRepository _workScheduleRepository;

        public WorkScheduleHandler(IWorkScheduleRepository workScheduleRepository , IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
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

            var shiftToUpdate = await _shiftRepository.GetByIdAsync(cmd.ShiftId);
            shiftToUpdate.EmployeeOnShift = cmd.Id;
            var result1 = await _shiftRepository.UpdateAsync(shiftToUpdate);
            
            return new IdResponse(assignedUser.Id);

        }
    }
}