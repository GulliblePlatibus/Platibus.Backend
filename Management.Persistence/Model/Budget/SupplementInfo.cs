using System;
using Dapper.FluentMap.Dommel.Mapping;
using Management.Persistence.Documents;

namespace Management.Persistence.Model.Budget
{
    public class SupplementInfoMap : DommelEntityMap<SupplementInfo>
    {
		
        public SupplementInfoMap()
        {
            ToTable("supplementinfo");
            Map(x => x.Id).ToColumn("id").IsKey();
            Map(x => x.Name).ToColumn("name");
            Map(x => x.Decription).ToColumn("decription");
            Map(x => x.Supplement).ToColumn("supplement");
            Map(x => x.SupplementDays).ToColumn("daysofsupplement");
            Map(x => x.TimeRange).ToColumn("timerange");



        }
    }
    
    
    public class SupplementInfo : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
        public double Supplement { get; set; }
        public string SupplementDays { get; set; }
        public string TimeRange { get; set; }
    }
}