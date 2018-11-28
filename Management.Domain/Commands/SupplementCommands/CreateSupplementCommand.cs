using System;
using System.Collections.Generic;
using Management.Domain.Documents;
using Management.Domain.DomainElements.BudgetPlanner.ValueObjects;

namespace Management.Domain.Commands.SupplementCommands
{
    public class CreateSupplementCommand : CommandWithIdResponse
    {
        public string Name { get; }
        public string Decription { get; }
        public double Supplement { get; }
        public HashSet<DayOfWeek> SupplementDays { get; }
        public List<HourInfo> TimeRange { get; }

        public CreateSupplementCommand(string name, string decription, bool isStaticSupplement, double supplement, HashSet<DayOfWeek> supplementDays, List<HourInfo> timeRange)
        {
            Name = name;
            Decription = decription;
            Supplement = supplement;
            SupplementDays = supplementDays;
            TimeRange = timeRange;
        }
    }
}