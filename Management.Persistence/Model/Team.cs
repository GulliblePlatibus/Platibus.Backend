using System.Collections.ObjectModel;

namespace Management.Persistence.Model
{
    public class Team
    {
        public Collection<Employee> TeamMembers { get; }
        public Manager TeamManager { get; set; }
        public string TeamName { get; set; }
        public string Description { get; set; }
    }
}