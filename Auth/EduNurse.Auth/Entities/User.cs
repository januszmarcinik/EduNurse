using System;

namespace EduNurse.Auth.Entities
{
    internal class User
    {
        public Guid Id { get; }
        public string Email { get; }
        public string PasswordHash { get; }
        public string PasswordSalt { get; }
        public DateTime CreatedDate { get; }

        public User(Guid id, string email, string passwordHash, string passwordSalt, DateTime createdDate)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            CreatedDate = createdDate;
        }
    }
}
