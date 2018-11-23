using System;
using System.Collections.Generic;
using Management.Domain.DomainElements.BudgetPlanner.ValueObjects;
using Management.Domain.QueryHandler;
using Management.Persistence.Model;
using Management.Persistence.Repositories;

namespace Management.Domain.DomainElements.BudgetPlanner
{
    public class Salary
    {
        public double BaseWage { get; private set; }
        public double Seniority { get; private set; }
        public double Night { get; private set; }
        public double Weekend { get; private set; }
        public double OddHours { get; set; }
        //  public float OverTime{ get; }
        public double Position { get; private set; }
        public double PaymentForShift { get; set; }
        public double TotalPaymentForShifts { get; set; }

        
        
        public Salary(User user)
        {
            BaseWage = user.BaseWage;
            Seniority = ResolveSeniority(user);
          //  OddHours = ResolveWorkHours();

            
          

        }
        
        public ShiftPayment ResolvePaymentsForShift(Shift shift)
        {
            var workHours = shift.Duration;
            
            var shiftPayment = new ShiftPayment();
 
            shiftPayment.TotalPayment =
                workHours * (BaseWage * ((BaseWage / 100) + Seniority + ResolveWorkHours(shift) + Position));


            shiftPayment.ShiftId = shift.Id;
            
            
            return shiftPayment;
            
        }

        public List<ShiftPayment> ResolvePaymentForShifts(IEnumerable<Shift> shifts)
        {
            var TotalShiftPayment = new List<ShiftPayment>();

            foreach (var VARIABLE in shifts)
            {
                TotalShiftPayment.Add(ResolvePaymentsForShift(VARIABLE));
                
                
            }

            return TotalShiftPayment;
        }
        
      
        
        
        private double ResolveSeniority(User user)
        {
           
            var currentTicks = DateTime.Today;
            var ticksInYear = (DateTime.Today.AddYears(1)).Subtract(currentTicks).Ticks;

            var ticksAtEmployment = user.EmploymentDate.Date.Ticks;

            var ticksSince = currentTicks.Ticks - ticksAtEmployment;

            var seniority = ticksSince / ticksInYear;

            var senioritySupplement = (seniority);

            return senioritySupplement;

        }
        
        private double ResolveWorkHours(Shift shift)
        {
           
            var startDate = shift.ShiftStart;
            var endDate = shift.ShiftEnd;

            var startDay = startDate.DayOfWeek;
            var endDay = endDate.DayOfWeek;

            var startHour = startDate.Hour;
            var endHour = endDate.Hour;

            var hourList = new List<int>();
        
            while (startHour != endHour)
            {
                if (startHour == 24)
                {
                    startHour = 0;
                }
            
                hourList.Add(startHour);
                
                startHour++;
            }
        
        var sortedWorkHours = new SortetWorkHours();
        
            var oddHourSupplement = 0.0;
            
            var nightHours = 0;
            
            var weekendHours = 0;

            var normalHours =  0;
            
            
            
            foreach (var VARIABLE in hourList)
            {

                if (VARIABLE >= 18 || VARIABLE <= 6)
                {
                    
                    nightHours++;
               
                    
                }

                else if (startDay == DayOfWeek.Saturday || startDay == DayOfWeek.Sunday && endDay == DayOfWeek.Saturday ||
                    endDay == DayOfWeek.Sunday)
                {
                    weekendHours++;

                }
                else
                {
                    normalHours++;
                }
            }

            oddHourSupplement = ((nightHours * 1.20) + (weekendHours * 1.40));
            

            return oddHourSupplement;

        }

    }
    
}