using System;
using Management.Persistence.Model;

namespace Management.API.RequestModels
{
    public class UpdateUserRequestModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRoles AccessLevel { get; set; }
        public double Wage { get; set; }
        public DateTime EmploymentDate { get; set; }
    }
}
