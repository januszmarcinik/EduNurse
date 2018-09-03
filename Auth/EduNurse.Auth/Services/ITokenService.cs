using EduNurse.Auth.Entities;

namespace EduNurse.Auth.Services
{
    internal interface ITokenService
    {
        TokenResult CreateToken(User user);
    }
}
