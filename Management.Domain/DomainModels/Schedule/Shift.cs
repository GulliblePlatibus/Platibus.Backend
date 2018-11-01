using System;

namespace Management.Domain.DomainModels.Schedule
{
    public class Shift
    {
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public ShiftType ShiftType { get; set; }
        public ShiftDetails Details { get; set; }

      /*  
        public Shift SplitShift(Employee splitWith)
        {
            
        }
      */
    }
}