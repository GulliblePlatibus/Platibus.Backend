using Management.Domain.DomainModels.Users;

namespace Management.Domain.DomainModels.Schedule
{
    public class ShiftDetails
    {
        public Employee OccupiedBy { get; set; }
        public Absence Absence { get; set; } //Review this
        public string Description { get; set; }
        
    }
}