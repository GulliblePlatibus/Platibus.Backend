namespace Management.Persistence.Model
{
    public class ShiftDetails
    {
        public Employee OccupiedBy { get; set; }
        public Absence Absence { get; set; } //Review this
        public string Description { get; set; }
        
    }
}