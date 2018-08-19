using System;
using System.Threading.Tasks;
using EduNurse.Api.Shared.Command;
using EduNurse.Authentication.Entities;
using EduNurse.Authentication.Services;
using EduNurse.Authentication.Shared.Commands;

namespace EduNurse.Authentication.CommandHandlers
{
    internal class RegisterCommandHandler : ICommandHandler<RegisterCommand>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordService _passwordService;

        public RegisterCommandHandler(IUsersRepository usersRepository, IPasswordService passwordService)
        {
            _usersRepository = usersRepository;
            _passwordService = passwordService;
        }

        public async Task HandleAsync(RegisterCommand command)
        {
            var task = _usersRepository.GetByEmailAsync(command.Email);

            var salt = _passwordService.GetSalt();
            var hash = _passwordService.GetHash(command.Password, salt);

            if (task.GetAwaiter().GetResult() != null)
            {
                throw new Exception("Email already exists.");
            }

            var user = new User(Guid.NewGuid(), command.Email, hash, salt, DateTime.Now);

            await _usersRepository.AddAsync(user);
        }
    }
}
