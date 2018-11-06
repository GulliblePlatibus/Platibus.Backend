namespace Management.API.Helpers
{
    public class CreateIdentityUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int AuthLevel { get; set; }
    }
}