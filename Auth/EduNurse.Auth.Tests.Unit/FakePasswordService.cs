using EduNurse.Auth.Services;

namespace EduNurse.Auth.Tests.Unit
{
    internal class FakePasswordService : IPasswordService
    {
        public string GetSalt() => "someComplicatedSalt0123456789";
        public string GetHash(string password, string salt) => "someCompicatedHash0123456789!*";
    }
}
