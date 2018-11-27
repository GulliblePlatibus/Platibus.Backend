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
    ICommandHandler<CreateShiftCommand, IdResponse> , ICommandHandler<AssignUserToShiftCommand , IdResponse> , ICommandHandler<DeleteShiftByIdCommand , IdResponse>
    {
        private IShiftRepository ShiftRepository { get; }


        public ShiftHandler(IShiftRepository shiftRepository)
        {
            ShiftRepository = shiftRepository;
        }
        
        public async Task<IdResponse> HandleAsync(CreateShiftCommand cmd, CancellationToken ct)
        {
            if (cmd.ShiftStart > cmd.ShiftEnd)
            {
                return IdResponse.Unsuccessful("cannot create a shift with an end time before the start time");
            }

            var id = Guid.NewGuid();

            var result = await ShiftRepository.InsertAsync(new Shift
            {
                Id = id,
                ShiftStart = cmd.ShiftStart,
                ShiftEnd = cmd.ShiftEnd,
               //Duration = cmd.ShiftEnd.Subtract(cmd.ShiftStart).TotalHours
               
            });
            
            return new IdResponse(id);
        }
        
        public async Task<IdResponse> HandleAsync(DeleteShiftByIdCommand cmd, CancellationToken ct)
        {
            if (cmd.Id.Equals(Guid.Empty))
            {
                return IdResponse.Unsuccessful("Id is empty");
            }
		    

            var shift = await ShiftRepository.GetByIdAsync(cmd.Id);

            var result = await ShiftRepository.DeleteByTAsync(shift);
            return IdResponse.Successful(shift.Id);
        }

        public async Task<IdResponse> HandleAsync(UpdateShiftCommand cmd, CancellationToken ct)
        {
            if (cmd.Id.Equals(Guid.Empty))
            {
                return IdResponse.Unsuccessful("User id is empty");
            }

            var Shift = new Shift
            {
                Id = cmd.Id,
                ShiftStart = cmd.ShiftStart,
                ShiftEnd = cmd.ShiftEnd,
            };

            var result = await ShiftRepository.UpdateAsync(Shift);
		    
		    
            return IdResponse.Successful(Shift.Id);
        }

        public async Task<IdResponse> HandleAsync(AssignUserToShiftCommand cmd, CancellationToken ct)
        {
            
            var updateShift = await ShiftRepository.GetByIdAsync(cmd.ShiftId);
            updateShift.ShiftEnd = cmd.EndTime;
            updateShift.ShiftStart = cmd.StartTime;
            updateShift.EmployeeOnShift = cmd.EmployeeId;

            var result = await ShiftRepository.UpdateAsync(updateShift);
            
            return IdResponse.Successful(updateShift.Id);
        }
    }
}