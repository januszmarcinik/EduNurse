using System;
using EduNurse.Auth.Services;

namespace EduNurse.Auth.Tests.Unit
{
    internal class SystemUnderTest : IDisposable
    {
        public IUsersRepository UsersRepository { get; }
        public IPasswordService PasswordService { get; }

        public SystemUnderTest()
        {
            UsersRepository = new FakeUsersRepository();
            PasswordService = new FakePasswordService();
        }

        public void Dispose()
        {
        }
    }
}
