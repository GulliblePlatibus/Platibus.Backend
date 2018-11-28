using System;
using System.Collections.Generic;
using Management.Documents.Documents;
using Management.Persistence.Model;
using SimpleSoft.Mediator;

namespace Management.Domain.Commands.ShiftCommands
{
    public class AddManyShiftsCommand : Command<IdResponse>
    {
        public List<Shift> ListOfShifts { get; set; }

        public AddManyShiftsCommand(List<Shift> listOfShifts)
        {
            ListOfShifts = listOfShifts;
        }

    }
}