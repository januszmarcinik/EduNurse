using System;
using System.Collections.Generic;
using System.Net.Http;
using EduNurse.Exams.Api;
using EduNurse.Exams.Api.Questions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace EduNurse.Exams.Tests.Integration
{
    internal class SystemUnderTest : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly IExamsContext _context;

        public SystemUnderTest()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>()
            );

            _context = _server.Host.Services.GetService<IExamsContext>();

            _client = _server.CreateClient();
        }

        public List<Question> SetQuestions(List<Question> questions)
        {
            _context.CreateMany(questions);
            _context.SaveChanges();

            return questions;
        }

        public ApiResponse HttpGet(string url)
        {
            return _client.GetAsync(url).GetApiResponse();
        }

        public ApiResponse HttpGet(string url, Guid id)
        {
            return _client.GetAsync($"{url}/{id}").GetApiResponse();
        }

        public void Dispose()
        {
            _server?.Dispose();
            _client?.Dispose();
            _context?.Dispose();
        }
    }
}
