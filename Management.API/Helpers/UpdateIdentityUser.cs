using Management.Persistence.Model;

namespace Management.API.Helpers
{
    public class UpdateIdentityUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRoles AuthLevel { get; set; }
    }
}