using Dapper.FluentMap.Dommel.Mapping;
using Management.Persistence.Documents;

namespace Management.Persistence.Model.Budget
{
    public class Salary : DommelEntityMap<Salary>
    {
            public float Wage { get; set; }
            public float Hours { get; set; }
        }
    }
