using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Management.Domain.DomainElements;
using Management.Domain.DomainElements.BudgetPlanner;
using Management.Domain.DomainElements.BudgetPlanner.ValueObjects;
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
            var shifts = await _shiftRepository.GetForUserWithIdAsync(query.UserId, query.FromtDate, query.ToDate);

            var salary = new Salary(user, SalaryConfigurationBuilder.Build(cfg =>
            {
                cfg.UseQuarterTimeScheduling();
                cfg.AddSupplement(
                    new SupplementInfo(
                        Guid.NewGuid(),
                        "Night Hour",
                        "Night Hour supplement for employee",
                        new List<DayOfWeek>{DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday},
                        new Supplement(false, 40),
                        new List<HourInfo>{new HourInfo(18, 0), new HourInfo(0,6)}));
                cfg.AddSupplement(
                    new SupplementInfo(
                        Guid.NewGuid(),
                        "Weekend Hours",
                        "Supplement for weekend hours",
                        new List<DayOfWeek>{DayOfWeek.Saturday, DayOfWeek.Sunday},
                        new Supplement(true, 20)));
                cfg.AddSupplement(
                    new SupplementInfo(
                        Guid.NewGuid(),
                        "Sunday Night",
                        "Supllement for sunday night hours",
                        new List<DayOfWeek>{DayOfWeek.Sunday},
                        new Supplement(true, 30)));
                
            }));
            
            var shiftPayments = salary.ResolvePaymentForShifts(shifts);
            
            return shiftPayments;
        }
    }
}