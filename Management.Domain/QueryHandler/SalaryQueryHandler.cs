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
using Salary = Management.Persistence.Model.Budget.Salary;

namespace Management.Domain.QueryHandler
{
    public class SalaryQueryHandler : IQueryHandler<GetWorkHoursForUser, Salary> , IQueryHandler<GetWageForUserWithId, Salary>, IQueryHandler<GetSalaryForUserWithId, Salary>
    {
        private ISalaryRepository SalaryRepository { get; }
        private IUserRepository UserRepository { get; }
        private IShiftRepository _shiftRepository { get; }

        public SalaryQueryHandler(ISalaryRepository salaryRepository, IUserRepository userRepository, IShiftRepository shiftRepository)
        {
            SalaryRepository = salaryRepository;
            UserRepository = userRepository;
            _shiftRepository = shiftRepository;
        }

        public async Task<Salary> HandleAsync(GetWorkHoursForUser query, CancellationToken ct)
        {

            return await SalaryRepository.GetWorkHoursForUserAsync(query.Id);
        }
        
        public async Task<Salary> HandleAsync(GetWageForUserWithId query, CancellationToken ct)
        {
            var result = await SalaryRepository.GetWageForUserWithIdAsync(query.UserId);
            return result;
        }
        
        
       
        public async Task<Salary> HandleAsync(GetSalaryForUserWithId query, CancellationToken ct)
        {

            var user = await UserRepository.GetByIdAsync(query.UserId);
            
            var shifts = _shiftRepository.GetForUserWithIdAsync(query.UserId).Result;
            
            var wageSupplements = new WageSupplements(user);
               
            var wage = user.BaseWage;
           
            var salary = new Salary();
           foreach (var shift in shifts)
            {
                wageSupplements.ResolveSupplement(user);
                
                salary.SalaryForPeriod += (float)(wage * shift.Duration) * wageSupplements.ResolveSupplement(user);

                salary.Sum += (float) shift.Duration;
            }

            salary.Wage = wage;
            salary.Id = query.UserId;

            
            return salary;

        }
    }
    
}