using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Management.Domain.Queries;
using Management.Domain.Queries.Shift;
using Management.Domain.Queries.WorkSchedule;
using Management.Persistence.Model;
using Management.Persistence.Repositories;
using SimpleSoft.Mediator;

namespace Management.Domain.QueryHandler
{
    public class ShiftQueryHandler : 
            IQueryHandler<GetAllShifts, IEnumerable<Shift>> , IQueryHandler<GetShiftById, Shift> , IQueryHandler<GetShiftsForUserWithId,IEnumerable<Shift>>
        {
            public IShiftRepository ShiftRepository { get; }

            public ShiftQueryHandler(IShiftRepository shiftRepository)
            {
                ShiftRepository = shiftRepository;
            }
        
            public async Task<IEnumerable<Shift>> HandleAsync(GetAllShifts query, CancellationToken ct)
            {
                var result = ShiftRepository.GetAllAsync();

                return await result;
            }

            public async Task<Shift> HandleAsync(GetShiftById query, CancellationToken ct)
            {
                var result = ShiftRepository.GetByIdAsync(query.Id);

                return await result;
            }
        
            public async Task<IEnumerable<Shift>> HandleAsync(GetShiftsForUserWithId query, CancellationToken ct)
            {
                var result = ShiftRepository.GetForUserWithIdAsync(query.Id, DateTime.MinValue, DateTime.MaxValue);
                
                return await result;
            }
        }
    }
    
