using System;
using System.Collections.Generic;
using Management.Persistence.Model;

namespace Management.Domain.DomainElements.BudgetPlanner
{
    public class WageSupplements
    {
        
        public float BaseWage { get; set; }
        public float Seniority { get; private set; }
        public float Night { get; private set; }
        public float Weekend { get; private set; }
      //  public float OverTime{ get; }
        public float Position { get; private set; }


        public WageSupplements(User user)
        {
            //Night = night;
            //Weekend = weekend;
           // Position = position;
            BaseWage = user.BaseWage;
            Seniority = ResolveSeniority(user);
        }

        public float ResolveSupplement(User user)
        {
            var supplement = ResolveSeniority(user);



            return (float) supplement;

        }
        
        
        
        

        private float ResolveSeniority(User user)
        {
           
            var currentTicks = DateTime.Today;
            var ticksInYear = (DateTime.Today.AddYears(1)).Subtract(currentTicks).Ticks;

            var ticksAtEmployment = user.EmploymentDate.Date.Ticks;

            var ticksSince = currentTicks.Ticks - ticksAtEmployment;

            var seniority = ticksSince / ticksInYear;

            var senioritySupplement = (((BaseWage / 100) * 1) * (seniority));

            return senioritySupplement;



        }

       /* private float ResolveNightHours(WorkSchedule workSchedule, DateTime timeOfDay)
        {
            if(shiftTime.Hour < )
        }
        */
        
    }
}