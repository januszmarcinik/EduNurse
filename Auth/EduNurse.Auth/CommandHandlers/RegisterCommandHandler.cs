using System;
using System.Threading.Tasks;
using EduNurse.Api.Shared;
using EduNurse.Api.Shared.Command;
using EduNurse.Auth.Entities;
using EduNurse.Auth.Services;
using EduNurse.Auth.Shared.Commands;

namespace EduNurse.Auth.CommandHandlers
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

        public async Task<Result> HandleAsync(RegisterCommand command)
        {
            var task = _usersRepository.GetByEmailAsync(command.Email);

            var salt = _passwordService.GetSalt();
            var hash = _passwordService.GetHash(command.Password, salt);

            if (task.GetAwaiter().GetResult() != null)
            {
                return Result.Failure("Email already exists.");
            }

            var user = new User(Guid.NewGuid(), command.Email, hash, salt, DateTime.Now);

            await _usersRepository.AddAsync(user);

            return Result.Success();
        }
    }
}
