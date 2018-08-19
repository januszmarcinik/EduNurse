using System;
using System.Threading.Tasks;
using EduNurse.Auth.CommandHandlers;
using EduNurse.Auth.Entities;
using EduNurse.Auth.Shared.Commands;
using FluentAssertions;
using Xunit;

namespace EduNurse.Auth.Tests.Unit
{
    public class UserCommandsTests
    {
        [Fact]
        public async Task RegisterUser_WhenEmailAlreadyExists_ShouldThrowAnException()
        {
            using (var sut = new SystemUnderTest())
            {
                var user = new User(Guid.NewGuid(), "some.email@edunurse.pl", "someHash", "someSalt", DateTime.Now);
                await sut.UsersRepository.AddAsync(user);

                var command = new RegisterCommand
                {
                    Email = "some.email@edunurse.pl",
                    Password = "someVeryHardPassword123",
                    ConfirmPassword = "someVeryHardPassword123"
                };

                var handler = new RegisterCommandHandler(sut.UsersRepository, sut.PasswordService);
                Func<Task> handle = async () => await handler.HandleAsync(command);

                handle.Should().Throw<Exception>().WithMessage("Email already exists.");
            }
        }

        [Fact]
        public async Task RegisterUser_WhenEmailDoesNotExists_ShouldCreateCorrectUser()
        {
            using (var sut = new SystemUnderTest())
            {
                var command = new RegisterCommand
                {
                    Email = "some.email@edunurse.pl",
                    Password = "someVeryHardPassword123",
                    ConfirmPassword = "someVeryHardPassword123"
                };

                var handler = new RegisterCommandHandler(sut.UsersRepository, sut.PasswordService);
                await handler.HandleAsync(command);

                var result = await sut.UsersRepository.GetByEmailAsync(command.Email);

                result.Id.Should().NotBeEmpty();
                result.Email.Should().Be(command.Email);
                result.PasswordHash.Should().NotBe(command.Password);
                result.PasswordSalt.Should().NotBe(command.Password);
                result.PasswordSalt.Should().NotBe(result.PasswordHash);
            }
        }
    }
}
