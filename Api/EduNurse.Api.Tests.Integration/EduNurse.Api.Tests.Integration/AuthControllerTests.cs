using System;
using System.Linq;
using System.Threading.Tasks;
using EduNurse.Auth;
using EduNurse.Auth.Entities;
using EduNurse.Auth.Shared.Commands;
using EduNurse.Auth.Shared.Results;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace EduNurse.Api.Tests.Integration
{
    public class AuthControllerTests
    {
        private const string Url = "api/v1/auth";

        [Fact]
        public async Task GetUserByEmail_WhenUserHasDifferentEmailAndIsNotAdmin_ShouldReturnAccessDenied()
        {
            using (var sut = new SystemUnderTest(UsersFactory.CreateUser("janusz@edunurse.pl")))
            {
                var user = UsersFactory.CreateUser("marta@edunurse.pl");
                await sut.CreateAsync(user);

                var url = $"{Url}/marta@edunurse.pl";
                var result = sut.HttpGet<UserResult>(url);

                result.IsSuccess.Should().BeFalse();
                result.Message.Should().BeEquivalentTo("Access denied.");
            }
        }

        [Fact]
        public async Task GetUserByEmail_WhenUserHasTheSameEmailAndIsNotAdmin_ShouldReturnCompletlyResult()
        {
            using (var sut = new SystemUnderTest(UsersFactory.CreateUser("janusz@edunurse.pl")))
            {
                var user = UsersFactory.CreateUser("janusz@edunurse.pl");
                await sut.CreateAsync(user);
                
                var url = $"{Url}/janusz@edunurse.pl";
                var result = sut.HttpGet<UserResult>(url);

                result.IsSuccess.Should().BeTrue();
            }
        }

        [Fact]
        public async Task GetUserByEmail_WhenUserHasDifferentEmailButIsAdmin_ShouldReturnCompletlyResult()
        {
            using (var sut = new SystemUnderTest(UsersFactory.CreateAdmin("janusz@edunurse.pl")))
            {
                var user = UsersFactory.CreateUser("marta@edunurse.pl");
                await sut.CreateAsync(user);

                var url = $"{Url}/marta@edunurse.pl";
                var result = sut.HttpGet<UserResult>(url);

                result.IsSuccess.Should().BeTrue();
            }
        }

        [Fact]
        public async Task RegisterUser_WhenEmailAlreadyExists_ShouldThrowAnException()
        {
            using (var sut = new SystemUnderTest(UsersFactory.CreateUser("janusz@edunurse.pl")))
            {
                var user = new User(Guid.NewGuid(), "janusz@edunurse.pl", true, Enumerable.Empty<Role>(), "zaq1@WSX-hash", "password-salt", DateTime.Now);
                await sut.CreateAsync(user);

                var command = new RegisterCommand
                {
                    Email = "janusz@edunurse.pl",
                    Password = "zaq1@WSX",
                    ConfirmPassword = "zaq1@WSX"
                };

                var url = $"{Url}/register";
                var result = sut.HttpPost(url, command);

                result.IsSuccess.Should().BeFalse();
                result.Message.Should().BeEquivalentTo("Email already exists.");
            }
        }

        [Fact]
        public async Task RegisterUser_WhenEmailDoesNotExists_ShouldCreateCorrectUser()
        {
            using (var sut = new SystemUnderTest(UsersFactory.CreateUser("janusz@edunurse.pl")))
            {
                var command = new RegisterCommand
                {
                    Email = "janusz@edunurse.pl",
                    Password = "zaq1@WSX",
                    ConfirmPassword = "zaq1@WSX"
                };

                var url = $"{Url}/register";
                var result = sut.HttpPost(url, command);

                var user = await sut.GetUserByEmailAsync(command.Email);

                result.IsSuccess.Should().BeTrue();
                user.Id.Should().NotBeEmpty();
                user.Email.Should().Be(command.Email);
                user.IsAdmin.Should().BeFalse();
                user.Roles.Should().BeEmpty();
                user.PasswordHash.Should().NotBe(command.Password);
                user.PasswordSalt.Should().NotBe(command.Password);
                user.PasswordHash.Should().NotBe(user.PasswordSalt);
            }
        }

        [Fact]
        public async Task SignInUser_WhenGivenCredentialsAreValid_ReturnSuccess()
        {
            using (var sut = new SystemUnderTest(UsersFactory.CreateUser("janusz@edunurse.pl")))
            {
                var user = UsersFactory.CreateUser("janusz@edunurse.pl");
                await sut.CreateAsync(user);

                var command = new SignInCommand(user.Email, UsersFactory.Password);

                var url = $"{Url}/sign-in";
                var result = sut.HttpPost(url, command);
                var token = JsonConvert.DeserializeObject<TokenResult>(result.Message);

                result.IsSuccess.Should().BeTrue();
                token.Token.Should().NotBeNullOrEmpty();
                token.Expiry.Should().BeAfter(DateTime.UtcNow);
            }
        }

        [Fact]
        public async Task SignInUser_WhenGivenCredentialsAreNotValid_ReturnFailure()
        {
            using (var sut = new SystemUnderTest(UsersFactory.CreateUser("janusz@edunurse.pl")))
            {
                await sut.CreateAsync(
                    new User(Guid.NewGuid(), "janusz@edunurse.pl", false, new [] { Role.AddExam }, "zaq1@WSX", "pass-salt", DateTime.Now)
                );

                var command = new SignInCommand("janusz@edunurse.pl", "bad-password");

                var url = $"{Url}/sign-in";
                var result = sut.HttpPost(url, command);

                result.IsSuccess.Should().BeFalse();
                result.Message.Should().BeEquivalentTo("Given email or password are not valid.");
            }
        }
    }
}
