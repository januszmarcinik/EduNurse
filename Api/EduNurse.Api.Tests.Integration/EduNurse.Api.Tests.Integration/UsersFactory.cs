using System;
using System.Linq;
using EduNurse.Auth;
using EduNurse.Auth.Entities;

namespace EduNurse.Api.Tests.Integration
{
    internal class UsersFactory
    {
        public const string Password = "zaq1@WSX";
        public const string PasswordHash = "AZ8LvuIdZ1LNIkmjDkn6NAJxZsMRTxkUrqTC2/CsyHi52qlPM2jkCg==";
        public const string PasswordSalt = "nCza2e56FuyLb5qHuHF8I+s5OfQqkbGI1cDp6Fmb6OA1xKMjYY0RHQ==";

        public static User CreateAdmin(string email)
        {
            return new User(Guid.NewGuid(), email, true, Enumerable.Empty<Role>(), PasswordHash, PasswordSalt, DateTime.Now);
        }

        public static User CreateUser(string email, params Role[] roles)
        {
            return new User(Guid.NewGuid(), email, false, roles, PasswordHash, PasswordSalt, DateTime.Now);
        }
    }
}
