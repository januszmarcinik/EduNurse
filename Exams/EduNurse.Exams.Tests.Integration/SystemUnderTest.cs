using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using EduNurse.Api.Shared;
using EduNurse.Exams.AzureTableStorage;
using EduNurse.Exams.Entities;
using EduNurse.Exams.Tests.Integration.Extensions;
using EduNurse.Exams.Tests.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace EduNurse.Exams.Tests.Integration
{
    internal class SystemUnderTest : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly IExamsRepository _examsRepository;

        public IMapper Mapper { get; }

        public SystemUnderTest()
        {
            var tableName = string.Concat(Guid.NewGuid().ToString().Where(char.IsLetter));

            _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseSetting("Exams:AzureTableStorage", "UseDevelopmentStorage=true")
                .UseSetting("Exams:ExamsTableName", tableName)
                .UseStartup<Api.Startup>()
            );

            _client = _server.CreateClient();

            _examsRepository = _server.Host.Services.GetService<IExamsRepository>();

            Mapper = AutoMapperConfiguration.GetMapper();
        }

        public async Task<Exam> CreateAsync(Exam entity)
        {
            await _examsRepository.AddAsync(entity);
            return entity;
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
            var context = _server.Host.Services.GetService<AtsExamsContext>();
            context.DeleteTableIfExistsAsync().GetAwaiter().GetResult();

            _server?.Dispose();
            _client?.Dispose();
        }
    }
}
