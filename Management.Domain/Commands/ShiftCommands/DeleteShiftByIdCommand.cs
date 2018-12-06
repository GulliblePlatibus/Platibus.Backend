using System;
using Management.Documents.Documents;
using SimpleSoft.Mediator;

namespace Management.Domain.Commands.ShiftCommands
{
        public class DeleteShiftByIdCommand : Command<IdResponse>
        {
            public Guid Id { get; set; }

            public DeleteShiftByIdCommand(Guid Id)
            {
                this.Id = Id;
            }
        }
    }
