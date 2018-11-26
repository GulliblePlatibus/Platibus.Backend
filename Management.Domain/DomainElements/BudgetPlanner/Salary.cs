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
        private readonly ISalaryConfiguration _salaryConfig;
        
        public double BaseWage { get; private set; }
        public SortedSupplements SortedSupplements { get; private set; }
        
        public Salary(User user, ISalaryConfiguration salaryConfig)
        {
            _salaryConfig = salaryConfig;
            BaseWage = user.BaseWage;
        }
        
        public ShiftPayment ResolvePaymentsForShift(Shift shift)
        {
            var resolvedWorkHours = ResolveWorkHours(shift);
            
            var shiftPayment = new ShiftPayment(Guid.Empty, Guid.Empty, resolvedWorkHours, SortedSupplements);
            
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
            double hours = 0; // 40 * 100
            double nightHours = 0; // 3 * 25
            double weekendHours = 0; // 12 * 30
            double nightWeekendHours = 0; //25 * 50

            Dictionary<SupplementInfo, double> ;
            
            foreach(var quarterHour in workHours)
            {
                hours += _salaryConfig.TimeTrackingInHours;

                var dayWeek = quarterHour.DayOfWeek;

                //Determine if the day is a weekend day
                if(dayWeek.Equals(DayOfWeek.Saturday) || dayWeek.Equals(DayOfWeek.Sunday))
                {
                    //Determine if the day just began in that case the last timeschedule is respective to the past day
                    if (quarterHour.DayBegan(DayOfWeek.Saturday))
                    {
                        nightHours += _salaryConfig.TimeTrackingInHours;
                        continue;
                    }

                    if (quarterHour.Hour >= _salaryConfig.WeekendNightBegin)
                    {
                        if (quarterHour.Minute == 0 && quarterHour.Hour == _salaryConfig.WeekendNightBegin)
                        {
                            continue;
                        }
                        nightWeekendHours += _salaryConfig.TimeTrackingInHours;
                    }
                    else if (quarterHour.Hour <= _salaryConfig.WeekendNightEnd)
                    {
                        if(quarterHour.Hour == _salaryConfig.WeekendNightEnd)
                        {
                            if (quarterHour.Minute == 0)
                            {
                                nightWeekendHours += _salaryConfig.TimeTrackingInHours;
                            }
                            else
                            {
                                weekendHours += _salaryConfig.TimeTrackingInHours;
                            }
                        }
                        else
                        {
                            nightWeekendHours += _salaryConfig.TimeTrackingInHours;
                        }
                    }
                    else
                    {
                        weekendHours += _salaryConfig.TimeTrackingInHours;
                    }
                }
                else
                {
                    if(quarterHour.DayBegan(DayOfWeek.Monday))
                    {
                        nightWeekendHours += _salaryConfig.TimeTrackingInHours;
                        continue;
                    }

                    if(quarterHour.Hour >= _salaryConfig.NightHourBegin)
                    {
                        if(quarterHour.Minute == 0 && quarterHour.Hour == _salaryConfig.NightHourBegin)
                        {
                            continue;
                        }
                        nightHours += _salaryConfig.TimeTrackingInHours;
                    }
                    else if (quarterHour.Hour <= _salaryConfig.NightHourEnd)
                    {
                        if(quarterHour.Minute == 0)
                        {
                            nightHours += _salaryConfig.TimeTrackingInHours;
                        }
                    }
                }
            }

            return new SortedWorkHours(hours, nightHours, weekendHours, nightWeekendHours);
        }
        
        private SortedWorkHours ResolvePaymentForHours(List<DateTime> workHours, int i)
        {
            double hours = 0; // 40 * 100

            var supplementHours = new Dictionary<SupplementInfo, double>();
            
            foreach(var quarterHour in workHours)
            {
                hours += _salaryConfig.TimeTrackingInHours;

                var dayWeek = quarterHour.DayOfWeek;

                //Determine if the day is a weekend day
                if(dayWeek.Equals(DayOfWeek.Saturday) || dayWeek.Equals(DayOfWeek.Sunday))
                {
                    //Determine if the day just began in that case the last timeschedule is respective to the past day
                    if (quarterHour.DayBegan(DayOfWeek.Saturday))
                    {
                        nightHours += _salaryConfig.TimeTrackingInHours;
                        continue;
                    }

                    if (quarterHour.Hour >= _salaryConfig.WeekendNightBegin)
                    {
                        if (quarterHour.Minute == 0 && quarterHour.Hour == _salaryConfig.WeekendNightBegin)
                        {
                            continue;
                        }
                        nightWeekendHours += _salaryConfig.TimeTrackingInHours;
                    }
                    else if (quarterHour.Hour <= _salaryConfig.WeekendNightEnd)
                    {
                        if(quarterHour.Hour == _salaryConfig.WeekendNightEnd)
                        {
                            if (quarterHour.Minute == 0)
                            {
                                nightWeekendHours += _salaryConfig.TimeTrackingInHours;
                            }
                            else
                            {
                                weekendHours += _salaryConfig.TimeTrackingInHours;
                            }
                        }
                        else
                        {
                            nightWeekendHours += _salaryConfig.TimeTrackingInHours;
                        }
                    }
                    else
                    {
                        weekendHours += _salaryConfig.TimeTrackingInHours;
                    }
                }
                else
                {
                    if(quarterHour.DayBegan(DayOfWeek.Monday))
                    {
                        nightWeekendHours += _salaryConfig.TimeTrackingInHours;
                        continue;
                    }

                    if(quarterHour.Hour >= _salaryConfig.NightHourBegin)
                    {
                        if(quarterHour.Minute == 0 && quarterHour.Hour == _salaryConfig.NightHourBegin)
                        {
                            continue;
                        }
                        nightHours += _salaryConfig.TimeTrackingInHours;
                    }
                    else if (quarterHour.Hour <= _salaryConfig.NightHourEnd)
                    {
                        if(quarterHour.Minute == 0)
                        {
                            nightHours += _salaryConfig.TimeTrackingInHours;
                        }
                    }
                }
            }

            return new SortedWorkHours(hours, nightHours, weekendHours, nightWeekendHours);
        }
    }
}