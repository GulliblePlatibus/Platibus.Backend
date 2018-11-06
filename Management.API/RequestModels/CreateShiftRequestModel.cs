using System;

namespace Management.API.RequestModels
{
    public class CreateShiftRequestModel
    {
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
    }
}