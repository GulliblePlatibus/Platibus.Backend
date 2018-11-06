using System;
using Management.Documents.Documents;
using SimpleSoft.Mediator;

namespace Management.Domain.Commands
{
    public class UpdateUserCommand : Command<IdResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Acceslevel { get; set; }
        public Guid Id { get; set; }

        public UpdateUserCommand(string name, string email, string password, int acceslevel , Guid Id)
        {
            Name = name;
            Email = email;
            Password = password;
            Acceslevel = acceslevel;
            this.Id = Id;
        }
        
    }
}