using System.Collections.ObjectModel;

namespace Management.Persistence.Model
{
    public class ShiftType
    {
        public string Name { get; set; }
        public Collection<Skill> Requirements { get; } //Skill requirements?
    }
}