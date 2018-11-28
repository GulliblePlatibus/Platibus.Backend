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

        public WorkScheduleHandler(IWorkScheduleRepository workScheduleRepository , IShiftRepository shiftRepository)
        {
            _workScheduleRepository = workScheduleRepository;
        }
        
        public async Task<IdResponse> HandleAsync(AssignUserToShiftCommand cmd, CancellationToken ct)
        {
            
            
            // if guid is empty delete employee from shift
            if (!cmd.Id.Equals(Guid.Empty))
            {
                var assignedUser = new WorkSchedule
                {
                    Id = cmd.Id,
                    ShiftId = cmd.ShiftId
                };

                var result = await _workScheduleRepository.InsertAsync(assignedUser);
                
                return new IdResponse(assignedUser.Id);
            }

            var shift = await _workScheduleRepository.GetByIdAsync(cmd.ShiftId);
            await _workScheduleRepository.DeleteByTAsync(shift);

           /* var shiftToUpdate = await _shiftRepository.GetByIdAsync(cmd.ShiftId);
            shiftToUpdate.EmployeeOnShift = cmd.Id;
            var result1 = await _shiftRepository.UpdateAsync(shiftToUpdate);
*/            
            return new IdResponse(Guid.Empty);

        }
    }
}
