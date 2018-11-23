using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Management.Domain.DomainElements.BudgetPlanner;
using Management.Domain.Queries;
using Management.Domain.Queries.Shift;
using Management.Domain.Queries.User;
using Management.Persistence.Model;
using Management.Persistence.Repositories;
using SimpleSoft.Mediator;

namespace Management.Domain.QueryHandler
{
    public class SalaryQueryHandler : IQueryHandler<GetSalaryForUserWithId, IEnumerable<ShiftPayment>>
    {
        
        private IUserRepository _userRepository { get; }
        private IShiftRepository _shiftRepository { get; }

        public SalaryQueryHandler(IUserRepository userRepository, IShiftRepository shiftRepository)
        {
            
            _userRepository = userRepository;
            _shiftRepository = shiftRepository;
        }
/*
        public async Task<Salary> HandleAsync(GetWorkHoursForUser query, CancellationToken ct)
        {

            return await SalaryRepository.GetWorkHoursForUserAsync(query.Id);
        }
        
        public async Task<Salary> HandleAsync(GetWageForUserWithId query, CancellationToken ct)
        {
            var result = await SalaryRepository.GetWageForUserWithIdAsync(query.UserId);
            return result;
        }
        
        */
       
        public async Task<IEnumerable<ShiftPayment>> HandleAsync(GetSalaryForUserWithId query, CancellationToken ct)
        {

            var user = await _userRepository.GetByIdAsync(query.UserId);
            
            var shifts = await _shiftRepository.GetForUserWithIdAsync(query.UserId);

            var salary = new Salary(user);
            
            
            return salary.ResolvePaymentForShifts(shifts);

        }
    }
    
}