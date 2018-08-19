using System;
using EduNurse.Authentication.Entities;
using Microsoft.WindowsAzure.Storage.Table;

namespace EduNurse.Authentication.AzureTableStorage
{
    internal class AtsUser : TableEntity
    {
        public AtsUser() { }

        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime CreatedDate { get; set; }

        public User ToDomainModel()
        {
            return new User(Guid.Parse(RowKey), Email, PasswordHash, PasswordSalt, CreatedDate.ToLocalTime());
        }
    }
}
