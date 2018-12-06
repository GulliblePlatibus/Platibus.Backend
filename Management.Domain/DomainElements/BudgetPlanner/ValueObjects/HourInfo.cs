using System;

namespace Management.Domain.DomainElements.BudgetPlanner.ValueObjects
{
    public class HourInfo
    {
        
        
        public int FromHour { get; }
        public int ToHour { get; }

        public HourInfo(int fromHour, int toHour)
        {
            if (fromHour > 23 || fromHour < 0)
            {
                throw new ArgumentException(nameof(fromHour) + " hour can't be less than 0 nor higher than 23");
            }
            
            if (toHour > 23 || toHour < 0)
            {
                throw new ArgumentException(nameof(toHour) + " hour can't be less than 0 nor higher than 23");
            }

            FromHour = fromHour;
            ToHour = toHour;
        }
    }
}