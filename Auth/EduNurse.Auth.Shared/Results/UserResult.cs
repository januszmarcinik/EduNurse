using System.Collections.Generic;

namespace EduNurse.Auth.Shared.Results
{
    public class UserResult
    {
        public UserResult(string email, bool isAdmin, IEnumerable<Role> roles)
        {
            Email = email;
            IsAdmin = isAdmin;
            Roles = roles;
        }

        public string Email { get; }
        public bool IsAdmin { get; }
        public IEnumerable<Role> Roles { get; }

        public class Role
        {
            public Role(string name, int value)
            {
                Name = name;
                Value = value;
            }

            public string Name { get; }
            public int Value { get; }
        }
    }
}
