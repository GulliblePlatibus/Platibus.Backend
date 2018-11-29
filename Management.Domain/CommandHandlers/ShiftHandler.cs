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
    ICommandHandler<CreateShiftCommand, IdResponse>  , ICommandHandler<AddManyShiftsCommand , IdResponse > , ICommandHandler<UpdateShiftCommand , IdResponse> ,
        ICommandHandler<DeleteShiftByIdCommand , IdResponse> , ICommandHandler<CreateShiftWithEmployeeCommand , IdResponse>
    {
        private readonly IWorkScheduleRepository _workScheduleRepository;
        private readonly IUserRepository _userRepository;
        private IShiftRepository ShiftRepository { get; }


        public ShiftHandler(IShiftRepository shiftRepository , IWorkScheduleRepository workScheduleRepository , IUserRepository userRepository)
        {
            _workScheduleRepository = workScheduleRepository;
            _userRepository = userRepository;
            ShiftRepository = shiftRepository;
        }
        
        public async Task<IdResponse> HandleAsync(CreateShiftCommand cmd, CancellationToken ct)
        {
            if (cmd.ShiftStart > cmd.ShiftEnd)
            {
                return IdResponse.Unsuccessful("cannot create a shift with an end time before the start time");
            }
            
            
            var id = Guid.NewGuid();
            
            

            if (cmd.EmployeeId.Equals(Guid.Empty))
            {
                
                var result = await ShiftRepository.InsertAsync(new Shift
                {
                    Id = id,
                    ShiftStart = cmd.ShiftStart,
                    ShiftEnd = cmd.ShiftEnd,
                    //Duration = cmd.ShiftEnd.Subtract(cmd.ShiftStart).TotalHours
               
                });
                return new IdResponse(id);
            }
            else
            {
                var allShiftswithEmployee = await _workScheduleRepository.GetUserShiftDetailed();

                foreach (var VARIABLE in allShiftswithEmployee)
                {
                    if (cmd.ShiftStart.Date.Equals(VARIABLE.shiftstart.Date) && VARIABLE.id.Equals(cmd.EmployeeId))
                    {
                        return new IdResponse(id);
                    }
                }
                
                var result = await ShiftRepository.InsertAsync(new Shift
                {
                    Id = id,
                    ShiftStart = cmd.ShiftStart,
                    ShiftEnd = cmd.ShiftEnd,
                    //Duration = cmd.ShiftEnd.Subtract(cmd.ShiftStart).TotalHours
               
                });
                
                

                var employeeOnShift = _workScheduleRepository.InsertAsync(new WorkSchedule
                {
                    ShiftId = id,
                    Id = cmd.EmployeeId
                });
                
                return new IdResponse(id);
            }
           
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
           

            var Shift = new Shift
            {
                Id = cmd.Id,
                ShiftStart = cmd.ShiftStart,
                ShiftEnd = cmd.ShiftEnd,
            };

            var shiftResult = await ShiftRepository.UpdateAsync(Shift);

            if (cmd.Employeeid.Equals(Guid.Empty))
            {
                var work = new WorkSchedule
                {
                    Id = cmd.Employeeid,
                    ShiftId = cmd.Id
                };

                _workScheduleRepository.DeleteByTAsync(work);
            }
            else
            {
                
                var allShiftswithEmployee = await _workScheduleRepository.GetUserShiftDetailed();

                foreach (var VARIABLE in allShiftswithEmployee)
                {
                    if (cmd.ShiftStart.Date.Equals(VARIABLE.shiftstart.Date) && VARIABLE.id.Equals(cmd.Employeeid))
                    {
                        return new IdResponse(cmd.Id);
                    }
                }
                
                var workschedule = new WorkSchedule
                {
                    Id = cmd.Employeeid,
                    ShiftId = cmd.Id
                
                };

                var hasshift = await _workScheduleRepository.UpdateAsync(workschedule);


                if (!hasshift)
                {
                    await _workScheduleRepository.InsertAsync(workschedule);
                }
            }
            
            
		    
            return IdResponse.Successful(Shift.Id);
        }


        public async Task<IdResponse> HandleAsync(AddManyShiftsCommand cmd, CancellationToken ct)
        {

            foreach (var VARIABLE in cmd.ListOfShifts)
            {
                VARIABLE.Id = Guid.NewGuid();
                await ShiftRepository.InsertAsync(VARIABLE);
            }
             

            return IdResponse.Successful(Guid.NewGuid());
        }

        public async Task<IdResponse> HandleAsync(CreateShiftWithEmployeeCommand cmd, CancellationToken ct)
        {
            
            var id = Guid.NewGuid();
            var shiftId = await ShiftRepository.InsertAsync(new Shift
            {
                Duration = 0.0,
                Id = id,
                ShiftEnd = cmd.Enddate,
                ShiftStart = cmd.StartDate
            });

            

            
            

            var result = await _workScheduleRepository.InsertAsync(new WorkSchedule
            {
                Id = cmd.EmployeeId,
                ShiftId = id
            });
            
            
            
            
            
            return IdResponse.Successful(id);
        }
    }
}
