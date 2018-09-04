using System;
using System.Collections.Generic;

namespace EduNurse.Auth.Entities
{
    internal class User
    {
        public Guid Id { get; }
        public string Email { get; }
        public bool IsAdmin { get; }
        public IEnumerable<Role> Roles { get; }
        public string PasswordHash { get; }
        public string PasswordSalt { get; }
        public DateTime CreatedDate { get; }

        public User(Guid id, string email, bool isAdmin, IEnumerable<Role> roles, string passwordHash, string passwordSalt, DateTime createdDate)
        {
            Id = id;
            Email = email;
            IsAdmin = isAdmin;
            Roles = roles;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            CreatedDate = createdDate;
        }
    }
}
