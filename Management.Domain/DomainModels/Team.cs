using System.Collections.ObjectModel;
using Management.Domain.DomainModels.Users;

namespace Management.Domain.DomainModels
{
    public class Team
    {
        public Collection<Employee> TeamMembers { get; }
        public Manager TeamManager { get; set; }
        public string TeamName { get; set; }
        public string Description { get; set; }
    }
}