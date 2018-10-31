using Management.Domain.Documents;

namespace Management.Domain.Commands.OrganizationCommands
{
    public class CreateOrganiztion : CommandWithIdResponse
    {
        public string Name { get; }
        public string Address { get; }

        public CreateOrganiztion(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }
}