namespace Management.Domain.DomainElements.BudgetPlanner.ValueObjects
{
    public class SortedSupplements
    {
        public double Seniority { get; }
        public double NightSupp { get; }
        public double WeekendSupp { get; }
        public double NightWeekendSupp { get; }


        public SortedSupplements(double seniority, double nightSupp, double weekendSupp, double nightWeekendSupp)
        {
            Seniority = seniority;
            NightSupp = nightSupp;
            WeekendSupp = weekendSupp;
            NightWeekendSupp = nightWeekendSupp;
        }
    }
}