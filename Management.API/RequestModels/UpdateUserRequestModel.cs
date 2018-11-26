namespace Management.API.RequestModels
{
    public class UpdateUserRequestModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Accesslevel { get; set; }
        public double Wage { get; set; }
    }
}