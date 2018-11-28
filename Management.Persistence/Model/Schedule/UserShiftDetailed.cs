using System;
using Management.Persistence.Documents;

namespace Management.Persistence.Model
{
    public class UserShiftDetailed 
    {
        
        
        
        public string name { get; set; }
        public DateTime shiftstart { get; set; }
        public DateTime Shiftend { get; set; }
        public Guid id { get; set; }


        public UserShiftDetailed()
        {
            
        }
        
    }
}