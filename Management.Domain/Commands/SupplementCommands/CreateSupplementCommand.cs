using Management.Domain.Documents;

namespace Management.Domain.Commands.SupplementCommands
{
    public class CreateSupplementCommand : CommandWithIdResponse
    {
        public string Name { get; }
        public string Decription { get; }
        public double Supplement { get; }
        public string SupplementDays { get; }
        public string TimeRange { get; }

        public CreateSupplementCommand(string name, string decription, double supplement, string supplementDays, string timeRange)
        {
            Name = name;
            Decription = decription;
            Supplement = supplement;
            SupplementDays = supplementDays;
            TimeRange = timeRange;
        }
    }
}