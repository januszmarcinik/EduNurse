using System;
using System.Collections.Generic;
using System.Linq;
using EduNurse.Auth.Entities;
using Microsoft.WindowsAzure.Storage.Table;

namespace EduNurse.Auth.AzureTableStorage
{
    internal class AtsUser : TableEntity
    {
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Roles { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime CreatedDate { get; set; }

        public User ToDomainModel()
        {
            return new User(Guid.Parse(RowKey), Email, IsAdmin, GetRoles(), PasswordHash, PasswordSalt, CreatedDate.ToLocalTime());
        }

        private IEnumerable<Role> GetRoles()
        {
            if (string.IsNullOrEmpty(Roles))
            {
                return Enumerable.Empty<Role>();
            }

            return Roles
                .Trim()
                .Split(',')
                .Select(int.Parse)
                .Select(r => (Role) r);
        }
    }
}
