using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Management.Documents.Documents;
using Management.Domain.Queries;
using Management.Domain.Queries.Shift;
using Management.Domain.Queries.WorkSchedule;
using Management.Persistence.Model;
using Management.Persistence.Repositories;
using SimpleSoft.Mediator;
using UserShiftDetailed = Management.Persistence.Model.UserShiftDetailed;

namespace Management.Domain.QueryHandler
{
    public class WorkScheduleQueryHandler : IQueryHandler<AllShiftsAndEmployeesQuery , List<AllShiftsWithEmployees>> , IQueryHandler<UserShiftDetailedQuery , List<UserShiftDetailed>>
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly IUserRepository _userRepository;
        public IWorkScheduleRepository WorkScheduleRepository { get; }
        

        public WorkScheduleQueryHandler(IWorkScheduleRepository _workScheduleRepository , IShiftRepository shiftRepository , IUserRepository userRepository)
        {
            _shiftRepository = shiftRepository;
            _userRepository = userRepository;
            WorkScheduleRepository = _workScheduleRepository;
        }


        public async Task<List<AllShiftsWithEmployees>> HandleAsync(AllShiftsAndEmployeesQuery query, CancellationToken ct)
        {
            var ShiftsWithEmployees = await WorkScheduleRepository.GetAllAsync();

            var AllShifts = await _shiftRepository.GetAllAsync();
            
            var resultList = new List<AllShiftsWithEmployees>();

            foreach (var shift in AllShifts) 
            {
                foreach (var ShiftWithEmployee in ShiftsWithEmployees)
                {
                    if (shift.Id.Equals(ShiftWithEmployee.ShiftId))
                    {
                        resultList.Add(new AllShiftsWithEmployees
                        {
                            StarTime = shift.ShiftStart,
                            EndTime = shift.ShiftEnd,
                            EmployeeOnShift = ShiftWithEmployee.Id,
                            Id = shift.Id
                            
                        });
                        
                        goto nextUpper;
                        
                    } 
                    
                }
                
                resultList.Add(new AllShiftsWithEmployees
                {
                    StarTime = shift.ShiftStart,
                    EndTime = shift.ShiftEnd,
                    EmployeeOnShift = Guid.Empty,
                    Id = shift.Id
                });
                
                nextUpper: ;
            }

            Console.WriteLine(resultList);
            return resultList;
        }


        public void addToList(List<AllShiftsWithEmployees> list , DateTime start , DateTime end , Guid ShiftId)
        {
            list.Add(new AllShiftsWithEmployees
            {
                StarTime = start,
                EndTime = end,
                EmployeeOnShift = Guid.Empty,
                Id = ShiftId
            });
        }

        public async Task<List<UserShiftDetailed>> HandleAsync(UserShiftDetailedQuery query, CancellationToken ct)
        {
            

            var result = await WorkScheduleRepository.GetUserShiftDetailed();



            return result.ToList();

        }
    }
}
