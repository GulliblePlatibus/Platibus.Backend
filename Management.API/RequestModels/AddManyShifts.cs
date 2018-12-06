using System.Collections.Generic;
using Management.Persistence.Model;

namespace Management.API.RequestModels
{
    public class AddManyShifts
    {
        public List<Shift> listOfShifts { get; set; }
    }
}