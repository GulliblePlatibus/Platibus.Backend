using System;
using Dapper.FluentMap.Dommel.Mapping;
using Management.Persistence.Documents;

namespace Management.Persistence.Model.Budget
{
    
    
    public class Salary : IEntity
    {
            public Guid Id { get; set; }
            public float Wage { get; set; }
            public float Sum { get; set; }
            public float SalaryForPeriod { get; set; }
    }
}
