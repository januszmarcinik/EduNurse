using System;
using EduNurse.Auth.Services;
using EduNurse.Auth.Tests.Unit.Fakes;

namespace EduNurse.Auth.Tests.Unit
{
    internal class SystemUnderTest : IDisposable
    {
        public IUsersRepository UsersRepository { get; }
        public IPasswordService PasswordService { get; }
        public ITokenService TokenService { get; }

        public SystemUnderTest()
        {
            UsersRepository = new FakeUsersRepository();
            PasswordService = new FakePasswordService();
            TokenService = new FakeTokenService();
        }

        public void Dispose()
        {
        }
    }
}
