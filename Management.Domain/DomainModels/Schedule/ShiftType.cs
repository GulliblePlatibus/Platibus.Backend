using System.Collections.ObjectModel;

namespace Management.Domain.DomainModels.Schedule
{
    public class ShiftType
    {
        public string Name { get; set; }
        public Collection<Skill> Requirements { get; } //Skill requirements?
    }
}