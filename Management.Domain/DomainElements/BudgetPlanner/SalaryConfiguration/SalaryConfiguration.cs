using System;
using System.Collections.Generic;
using Management.Domain.DomainElements.BudgetPlanner.ValueObjects;

namespace Management.Domain.DomainElements.BudgetPlanner
{
    public interface ISalaryConfiguration
    {
        int TimeTrackingIntervalInMinutes { get; }
        double TimeTrackingInHours { get; }
        List<SupplementInfo> Supplements { get; }
        
    }
    
    public class SalaryConfiguration : ISalaryConfiguration
    {
        private int timeTrackingIntervalInMinutes = 15;
        
        public double TimeTrackingInHours { get; private set; }
        public List<SupplementInfo> Supplements { get; } = new List<SupplementInfo>();

        public void AddSupplement(SupplementInfo supplement)
        {
            Supplements.Add(supplement);
        }

        public int TimeTrackingIntervalInMinutes
        {
            get
            {
                return timeTrackingIntervalInMinutes;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(nameof(TimeTrackingIntervalInMinutes) + " Time tracking is not allowed to be below 0");
                }

                timeTrackingIntervalInMinutes = value;
                TimeTrackingInHours = value / 60.0;
            }
        }
    }
    
    public class SalaryConfigurationBuilder
    {
        public static SalaryConfiguration Build(Action<SalaryConfiguration> ctx)
        {
            SalaryConfiguration salaryConfig = new SalaryConfiguration();

            ctx(salaryConfig);

            return salaryConfig;
        }
    }

    public class SupplementInfo
    {
        public Guid Id { get; }

        public string Name { get; }
        public string Description { get; }
        public Supplement Supplement { get; }
        public List<DayOfWeek> DayOfSupplement { get; }
        public List<HourInfo> TimeRanges { get; }

        public SupplementInfo(Guid id, string name, string description, List<DayOfWeek> dayOfSupplement, Supplement supplement, List<HourInfo> timeRanges = null)
        {
            Id = id;
            Name = name;
            Description = description;
            DayOfSupplement = dayOfSupplement;
            Supplement = supplement;
            TimeRanges = timeRanges;
        }
    }
}