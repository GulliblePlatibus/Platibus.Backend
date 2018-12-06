using System;
using Management.Domain.DomainElements.BudgetPlanner.ValueObjects;
using Management.Persistence.Model;

namespace Management.Domain.DomainElements.BudgetPlanner
{
    public class ShiftPayment
    {
        public Guid UserId { get; }
        public double Seniority { get; }
        public double BaseWage { get; }
        public Shift Shift { get; }
        public SortedWorkHours SortedWorkHours { get; }

        public double TotalPayment { get; private set; }
        
        public ShiftPayment(Guid userId, double seniority, double baseWage, Shift shift, SortedWorkHours sortedWorkHours)
        {
            UserId = userId;
            Seniority = seniority;
            BaseWage = baseWage;
            Shift = shift;
            SortedWorkHours = sortedWorkHours;
            
            CalculateTotalPayment();
        }


        private void CalculateTotalPayment()
        {
            var calculatedBaseWage = BaseWage + (((BaseWage / 100) * 3) * Seniority);

            var totalPaymentForHours = SortedWorkHours.Hours * calculatedBaseWage;

            foreach (var supplementHour in SortedWorkHours.SupplementHours)
            {
                var suppInfo = supplementHour;
                var suppHours = supplementHour.Value;
                var supplement = suppInfo.Supplement;
                

                if (suppInfo.Supplement.IsStaticSupplement)
                {
                    totalPaymentForHours += suppHours * supplement.Amount;
                }
                else
                {
                    var supplementPercentage = supplement.Amount;

                    var amount = BaseWage / 100 * supplementPercentage;

                    totalPaymentForHours += amount * suppHours;

                }
            }

            TotalPayment = totalPaymentForHours;
        }
    }
}