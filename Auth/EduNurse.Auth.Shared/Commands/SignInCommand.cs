using EduNurse.Api.Shared.Command;

namespace EduNurse.Auth.Shared.Commands
{
    public class SignInCommand : ICommand
    {
        public string Email { get; }
        public string Password { get; }

        public SignInCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
