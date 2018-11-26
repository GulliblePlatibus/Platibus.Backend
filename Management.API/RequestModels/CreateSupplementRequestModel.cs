using System;

namespace Management.API.RequestModels
{
    public class CreateSupplementRequestModel
    {
        public string Name { get; set; }
        public string Decription { get; set; }
        public double Supplement { get; set; }
        public string SupplementDays { get; set; }
        public string TimeRange { get; set; }
    }
}