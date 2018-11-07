using System;

namespace Management.API.RequestModels
{
    public class UpdateShiftRequestModel
    {
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
    }
}