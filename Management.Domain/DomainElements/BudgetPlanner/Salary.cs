using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Management.Domain.DomainElements.BudgetPlanner.ValueObjects;
using Management.Domain.QueryHandler;
using Management.Persistence.Model;
using Management.Persistence.Repositories;

namespace Management.Domain.DomainElements.BudgetPlanner
{
    public class Salary
    {
        private readonly ISalaryConfiguration _salaryConfig;
        
        public User User { get; private set; }
        public SortedSupplements SortedSupplements { get; private set; }
        
        public Salary(User user, ISalaryConfiguration salaryConfig)
        {
            _salaryConfig = salaryConfig;
            User = user;
        }
        
        public ShiftPayment ResolvePaymentsForShift(Shift shift)
        {
            var resolvedWorkHours = ResolveWorkHours(shift);
            
            var shiftPayment = new ShiftPayment(User.Id, shift.Id, resolvedWorkHours);
            
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
            var ticksInYear = DateTime.Today.AddYears(1).Subtract(currentTicks).Ticks;

            var ticksAtEmployment = user.EmploymentDate.Date.Ticks;

            var ticksSince = currentTicks.Ticks - ticksAtEmployment;

            var seniority = ticksSince / ticksInYear;

            var senioritySupplement = seniority;

            return senioritySupplement;
        }
        
        private SortedWorkHours ResolveWorkHours(Shift shift)
        {
            var startDate = shift.ShiftStart;

            var currentHour = new DateTime(startDate.Ticks).AddMinutes(_salaryConfig.TimeTrackingIntervalInMinutes);

            var endDate = shift.ShiftEnd;

            var hourList = new List<DateTime>();

            while(DateTime.Compare(currentHour, endDate) <= 0)
            {
                var hour = new DateTime(currentHour.Ticks);

                hourList.Add(hour);

                currentHour = new DateTime(hour.Ticks).AddMinutes(_salaryConfig.TimeTrackingIntervalInMinutes);
            }

            return ResolvePaymentForHours(hourList);
        }
        
        
        
        private SortedWorkHours ResolvePaymentForHours(List<DateTime> workHours)
        {
            double hours = 0;
            var supplementHours = new Dictionary<SupplementInfo, double>();
            
            foreach(var quarterHour in workHours)
            {
                hours += _salaryConfig.TimeTrackingInHours;

                var dayWeek = quarterHour.DayOfWeek;
                var timeScheduling = new DateTime(quarterHour.Ticks);
                
                //Check if date is dateBegan
                if (quarterHour.DayBegan())
                {
                    //Since day is dayBegan we should consider the day as the past day
                    dayWeek = dayWeek.PastDay();
                    
                    //This also means that we should set time schedule to 23:59 instead of 00:00 in order to get the correct date in the following section
                    timeScheduling.SubstractMinute(1);
                }

                //Now that we've sorted dayWeek and timeScheduling foreach supplement and determine which supplements the current employee is eligible to receive respective to his work(his shift) 
                foreach (var supplementInfo in _salaryConfig.Supplements)
                {
                    //Is current time Schedule within the applicable day for this respective supplement
                    if (supplementInfo.DayOfSupplement.Contains(dayWeek))
                    {
                        //Check if the supplement has any hour ranges meaning it is either null or empty
                        if (!supplementInfo.TimeRanges.IsNullOrEmpty())
                        {
                            var hourOfTimeSchedule = timeScheduling.Hour;
                            var minuteOfTimeSchedule = timeScheduling.Minute;
                            
                            //There exists a time ranges required to receive the supplement
                            foreach (var timeRange in supplementInfo.TimeRanges)
                            {
                                var hoursBetween = GetHoursBetween(timeRange.FromHour, timeRange.ToHour);
                                
                                if (hoursBetween.Contains(hourOfTimeSchedule))
                                {
                                    if (timeRange.FromHour == hourOfTimeSchedule && minuteOfTimeSchedule == 0)
                                    {
                                        //We are not actually within range only on the verge the work has been down from 15 minutes before and leading up to fromHour
                                        continue;
                                    }

                                    if (timeRange.ToHour == hourOfTimeSchedule && minuteOfTimeSchedule != 0)
                                    {
                                        //We are not actually within the range only because hours is int and thereby if we reach to hour == hourOfTimeSchedule this doesn't necesarilly means that the minutes arent :55 meaning 55 minutes pass the hour of discurse
                                        continue;
                                    }
                                    
                                    //We are later or equal to start of range or equal to or lower than end range
                                    if (supplementHours.ContainsKey(supplementInfo))
                                    {
                                        supplementHours[supplementInfo] += _salaryConfig.TimeTrackingInHours;
                                    }
                                    else
                                    {
                                        supplementHours.Add(supplementInfo, _salaryConfig.TimeTrackingInHours);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Since there are no time range requirements for this supplement he is eligible to receive the supplement by day
                            if (supplementHours.ContainsKey(supplementInfo))
                            {
                                supplementHours[supplementInfo] += _salaryConfig.TimeTrackingInHours;
                            }
                            else
                            {
                                supplementHours.Add(supplementInfo, _salaryConfig.TimeTrackingInHours);    
                            }
                        }
                    }
                }
            }

            return new SortedWorkHours(hours, supplementHours);
        }

        private int[] GetHoursBetween(int fromHour, int toHour)
        {
            var list = new List<int>();

            while (fromHour != toHour)
            {
                list.Add(fromHour);
                fromHour++;
                if (fromHour == 24)
                {
                    fromHour = 0;
                }
            }
            list.Add(toHour);

            return list.ToArray();
        }
    }
}