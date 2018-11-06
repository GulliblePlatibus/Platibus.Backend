using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Management.Domain.Queries;
using Management.Domain.Queries.Shift;
using Management.Persistence.Model;
using Management.Persistence.Repositories;
using SimpleSoft.Mediator;

namespace Management.Domain.QueryHandler
{
    public class ShiftQueryHandler : 
            IQueryHandler<GetAllShifts, IEnumerable<Shift>> , IQueryHandler<GetShiftById, Shift>
        {
            public IShiftRepository ShiftRepository { get; }

            public ShiftQueryHandler(IShiftRepository shiftRepository)
            {
                ShiftRepository = shiftRepository;
            }
        
            public Task<IEnumerable<Shift>> HandleAsync(GetAllShifts query, CancellationToken ct)
            {
                var result = ShiftRepository.GetAllAsync();

                return result;
            }

            public Task<Shift> HandleAsync(GetShiftById query, CancellationToken ct)
            {
                var result = ShiftRepository.GetByIdAsync(query.Id);

                return result;
            }
        }
    }
    
