using EduNurse.Auth.Services;

namespace EduNurse.Auth.Tests.Unit
{
    internal class FakePasswordService : IPasswordService
    {
        public string GetSalt() => "password-salt";
        public string GetHash(string password, string salt) => $"{password}-hash";
    }
}
