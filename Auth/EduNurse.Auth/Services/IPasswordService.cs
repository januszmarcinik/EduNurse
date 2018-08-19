namespace EduNurse.Auth.Services
{
    internal interface IPasswordService
    {
        string GetSalt();
        string GetHash(string password, string salt);
    }
}
