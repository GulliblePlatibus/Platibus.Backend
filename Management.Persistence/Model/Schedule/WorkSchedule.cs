using System;
using Dapper.FluentMap.Dommel.Mapping;
using Management.Persistence.Documents;

namespace Management.Persistence.Model
{
    public class WorkScheduleMap : DommelEntityMap<WorkSchedule>
    {
        public WorkScheduleMap()
        {
            ToTable("hasshift");
            Map(x => x.ShiftId).ToColumn("shiftid").IsKey();
            Map(x => x.Id).ToColumn("employeeid");

        }
    }

    public class WorkSchedule : IEntity
    {
        public Guid Id { get; set; }
        public Guid ShiftId { get; set; }
        
     
    }
}