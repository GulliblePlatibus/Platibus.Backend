using Management.Persistence.Model;
using SimpleSoft.Mediator;

namespace Management.Queries.Queries
{
    public class LoginQuery : Query<User>
    {
        public string Email { get; }
        public string Password { get; }

        public LoginQuery(string email , string password)
        {
            Email = email;
            Password = password;
        }
        
        
    }
}