using System;

namespace Management.Acquaintance
{
    public interface IUser
    {
        Guid Id { get; }
        string Email { get; set; }
        string Password { get; set; }
    }
}