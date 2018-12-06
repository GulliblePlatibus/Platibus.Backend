using System;
using Dapper.FluentMap.Dommel.Mapping;
using Management.Persistence.Documents;

namespace Management.Persistence.Model.Budget
{
    public class AssignedSuppMap : DommelEntityMap<AssignedSupplements>
    {
        public AssignedSuppMap()
        {
            ToTable("hassupplement");
            Map(x => x.Id).ToColumn("userid");
            Map(x => x.SuppId).ToColumn("supplementid");

        }
    }   
    public class AssignedSupplements : IEntity
    {
        public Guid Id { get; set; }
        public Guid SuppId { get; set; }
    }
}