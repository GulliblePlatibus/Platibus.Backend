using System;

namespace Management.Persistence.Model
{
    public class Shift
    {
        public Guid Id { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        

      /*  
        public Shift SplitShift(Employee splitWith)
        {
            
        }
      */
    }
}