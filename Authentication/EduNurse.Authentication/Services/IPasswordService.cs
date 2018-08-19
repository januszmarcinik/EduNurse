namespace EduNurse.Authentication.Services
{
    internal interface IPasswordService
    {
        string GetSalt();
        string GetHash(string password, string salt);
    }
}
