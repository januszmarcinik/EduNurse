using System;
using EduNurse.Auth.Entities;
using EduNurse.Auth.Services;

namespace EduNurse.Auth.Tests.Unit.Fakes
{
    internal class FakeTokenService : ITokenService
    {
        public TokenResult CreateToken(User user)
        {
            return new TokenResult("token", DateTime.Now.AddMinutes(45));
        }
    }
}
