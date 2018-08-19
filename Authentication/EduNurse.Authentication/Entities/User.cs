using System;

namespace EduNurse.Authentication.Entities
{
    internal class User
    {
        public Guid Id { get; }
        public string Email { get; }
        public string PasswordHash { get; }
        public string PasswordSalt { get; }

        public User(Guid id, string email, string passwordHash, string passwordSalt)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
    }
}
