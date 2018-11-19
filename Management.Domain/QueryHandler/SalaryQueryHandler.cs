using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Management.Domain.Queries;
using Management.Domain.Queries.Shift;
using Management.Domain.Queries.User;
using Management.Persistence.Model.Budget;
using Management.Persistence.Repositories;
using SimpleSoft.Mediator;

namespace Management.Domain.QueryHandler
{
    public class SalaryQueryHandler : IQueryHandler<GetWorkHoursForUser, Salary> , IQueryHandler<GetWageForUserWithId, Salary>, IQueryHandler<GetSalaryForUserWithId, Salary>
    {
        
        public ISalaryRepository SalaryRepository { get; }

        public SalaryQueryHandler(ISalaryRepository salaryRepository)
        {
            SalaryRepository = salaryRepository;
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
            
            var wage = await SalaryRepository.GetWageForUserWithIdAsync(query.UserId);
            
            var hours = await SalaryRepository.GetWorkHoursForUserAsync(query.UserId);

            var result = wage.Wage * hours.Sum;
            
            var salary = new Salary();
            salary.SalaryForPeriod = result;
            salary.Sum = hours.Sum;
            salary.Wage = wage.Wage;
            salary.Id = query.UserId;
            
            return salary;

        }
    }
    
}