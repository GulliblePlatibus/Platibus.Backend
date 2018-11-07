using System;

namespace Management.API.RequestModels
{
    public class AssignShiftRequestModel
    {
        public Guid Id { get; set; }
        public Guid ShiftId { get; set; }
    }
}