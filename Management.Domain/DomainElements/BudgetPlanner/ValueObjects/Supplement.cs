namespace Management.Domain.DomainElements.BudgetPlanner.ValueObjects
{
    public class Supplement
    {
        public bool IsStaticSupplement { get; }
        public double Amount { get; }


        public Supplement(bool isStaticSupplement, double amount)
        {
            IsStaticSupplement = isStaticSupplement;
            Amount = amount;
        }
    }
}