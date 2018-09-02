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
                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().BeFalse();
                result.Message.Should().BeEquivalentTo("Email already exists.");
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
                result.PasswordHash.Should().BeEquivalentTo($"{command.Password}-hash");
                result.PasswordSalt.Should().NotBe(command.Password);
                result.PasswordSalt.Should().NotBe(result.PasswordHash);
            }
        }

        [Fact]
        public async Task SignInUser_WhenGivenCredentialsAreValid_ReturnSuccess()
        {
            using (var sut = new SystemUnderTest())
            {
                await sut.UsersRepository.AddAsync(
                    new User(Guid.NewGuid(), "janusz@edunurse.pl", "zaq1@WSX-hash", "password-salt", DateTime.Now)
                );

                var command = new SignInCommand("janusz@edunurse.pl", "zaq1@WSX");
                var handler = new SignInCommandHandler(sut.UsersRepository, sut.PasswordService);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().BeTrue();
            }
        }

        [Fact]
        public async Task SignInUser_WhenGivenCredentialsAreNotValid_ReturnFailure()
        {
            using (var sut = new SystemUnderTest())
            {
                await sut.UsersRepository.AddAsync(
                    new User(Guid.NewGuid(), "janusz@edunurse.pl", "zaq1@WSX", "pass-salt", DateTime.Now)
                );

                var command = new SignInCommand("janusz@edunurse.pl", "bad-password");
                var handler = new SignInCommandHandler(sut.UsersRepository, sut.PasswordService);

                var result = await handler.HandleAsync(command);

                result.IsSuccess.Should().BeFalse();
                result.Message.Should().BeEquivalentTo("Given email or password are not valid.");
            }
        }
    }
}
