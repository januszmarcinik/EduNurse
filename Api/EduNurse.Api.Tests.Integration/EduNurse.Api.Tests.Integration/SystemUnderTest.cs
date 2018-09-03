using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using EduNurse.Api.Shared;
using EduNurse.Api.Tests.Integration.Extensions;
using EduNurse.Auth;
using EduNurse.Auth.AzureTableStorage;
using EduNurse.Auth.Entities;
using EduNurse.Auth.Services;
using EduNurse.Exams;
using EduNurse.Exams.AzureTableStorage;
using EduNurse.Exams.Entities;
using EduNurse.Exams.Tests.Unit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace EduNurse.Api.Tests.Integration
{
    internal class SystemUnderTest : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        private readonly IExamsRepository _examsRepository;
        private readonly IUsersRepository _usersRepository;

        public IMapper Mapper { get; }

        public SystemUnderTest()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseSetting("Exams:AzureTableStorage", "UseDevelopmentStorage=true")
                .UseSetting("Exams:ExamsTableName", GetRandomNameForTable())
                .UseSetting("Auth:AzureTableStorage", "UseDevelopmentStorage=true")
                .UseSetting("Auth:AuthTableName", GetRandomNameForTable())
                .UseSetting("Jwt:Issuer", "test")
                .UseSetting("Jwt:Key", "DSdmsadsd8sd8dD@(@MD9msa9mdm9m91d112x019mx8291m")
                .UseSetting("Jwt:ExpiryMinutes", "1")
                .UseStartup<Startup>()
            );

            _client = _server.CreateClient();

            _examsRepository = _server.Host.Services.GetService<IExamsRepository>();
            _usersRepository = _server.Host.Services.GetService<IUsersRepository>();

            SetupAuthorizationHeader();

            Mapper = new MapperConfiguration(x =>
            {
                x.AddProfile<ExamsTestMappings>();
                x.AddProfile<AuthMappings>();
            }).CreateMapper();
        }

        public async Task<Exam> CreateAsync(Exam entity)
        {
            await _examsRepository.AddAsync(entity);
            return entity;
        }

        public async Task<User> CreateAsync(User user)
        {
            await _usersRepository.AddAsync(user);
            return user;
        }

        public async Task<List<Exam>> CreateManyAsync(List<Exam> entities)
        {
            foreach (var e in entities)
            {
                await _examsRepository.AddAsync(e);
            }

            return entities;
        }

        public async Task<Exam> GetExamByIdAsync(Guid id)
        {
            return await _examsRepository.GetByIdAsync(id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _usersRepository.GetByEmailAsync(email);
        }

        public async Task<List<Exam>> GetAllExamsAsync()
        {
            var atsRepository = (AtsExamsRepository) _examsRepository;
            return (await atsRepository.GetAllExamsAsync()).ToList();
        }

        public Result<T> HttpGet<T>(string url)
        {
            return _client.GetAsync(url).GetResult<T>();
        }

        public Result<T> HttpGet<T>(string url, Guid id)
        {
            return _client.GetAsync($"{url}/{id}").GetResult<T>();
        }

        public Result<string> HttpPost(string url, object body)
        {
            return _client.PostAsync(url, body.ToStringContent()).GetResult<string>();
        }

        public Result<string> HttpPut(string url, Guid id, object body)
        {
            return _client.PutAsync($"{url}/{id}", body.ToStringContent()).GetResult<string>();
        }

        public Result<string> HttpDelete(string url, Guid id)
        {
            return _client.DeleteAsync($"{url}/{id}").GetResult<string>();
        }

        public void Dispose()
        {
            var examsContext = _server.Host.Services.GetService<AtsExamsContext>();
            examsContext.DeleteTableIfExistsAsync().GetAwaiter().GetResult();

            var usersContext = _server.Host.Services.GetService<AtsUsersContext>();
            usersContext.DeleteTableIfExistsAsync().GetAwaiter().GetResult();

            _server?.Dispose();
            _client?.Dispose();
        }

        private static string GetRandomNameForTable()
        {
            return string.Concat(Guid.NewGuid().ToString().Where(char.IsLetter));
        }

        private void SetupAuthorizationHeader()
        {
            var tokenService = _server.Host.Services.GetService<ITokenService>();
            var user = TestUser.CreateUser();
            var token = tokenService.CreateToken(user);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);
        }
    }
}
