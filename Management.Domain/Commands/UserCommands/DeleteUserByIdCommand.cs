using System;
using Management.Documents.Documents;
using SimpleSoft.Mediator;

namespace Management.Domain.Commands
{
    public class DeleteUserByIdCommand : Command<IdResponse>
    {
        public Guid Id { get; set; }

        public DeleteUserByIdCommand(Guid Id)
        {
            this.Id = Id;
        }
    }
}