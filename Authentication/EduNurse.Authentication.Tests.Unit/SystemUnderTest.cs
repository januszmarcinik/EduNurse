using System;
using EduNurse.Authentication.Services;

namespace EduNurse.Authentication.Tests.Unit
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
