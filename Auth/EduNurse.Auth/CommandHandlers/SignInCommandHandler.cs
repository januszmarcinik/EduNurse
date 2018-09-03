using System.Threading.Tasks;
using EduNurse.Api.Shared;
using EduNurse.Api.Shared.Command;
using EduNurse.Auth.Services;
using EduNurse.Auth.Shared.Commands;
using Newtonsoft.Json;

namespace EduNurse.Auth.CommandHandlers
{
    internal class SignInCommandHandler : ICommandHandler<SignInCommand>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public SignInCommandHandler(IUsersRepository usersRepository, IPasswordService passwordService, ITokenService tokenService)
        {
            _usersRepository = usersRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public async Task<Result> HandleAsync(SignInCommand command)
        {
            var user = await _usersRepository.GetByEmailAsync(command.Email);
            if (user != null)
            {
                var hash = _passwordService.GetHash(command.Password, user.PasswordSalt);
                if (user.PasswordHash == hash)
                {
                    var token = _tokenService.CreateToken(user);
                    return Result.Success(JsonConvert.SerializeObject(token));
                }
            }

            return Result.Failure("Given email or password are not valid.");
        }
    }
}
