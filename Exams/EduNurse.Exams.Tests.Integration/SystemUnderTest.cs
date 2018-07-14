using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using EduNurse.Exams.Api;
using EduNurse.Exams.Api.Entities;
using EduNurse.Exams.Tests.Integration.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EduNurse.Exams.Tests.Integration
{
    internal class SystemUnderTest : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        private readonly ExamsContext _context;

        public SystemUnderTest()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>()
            );
            _client = _server.CreateClient();
            _context = _server.Host.Services.GetService<ExamsContext>();
        }

        public T Create<T>(T entity) where T : Entity
        {
            _context.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public List<T> CreateMany<T>(List<T> entities) where T : Entity
        {
            _context.AddRange(entities);
            _context.SaveChanges();

            return entities;
        }

        public Exam GetExamById(Guid id, bool onScope = false)
        {
            if (onScope)
            {
                using (var scope = _server.Host.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<ExamsContext>();
                    return context.Exams.Include(x => x.Questions).SingleOrDefault(x => x.Id == id);
                }
            }

            return _context.Exams.Include(x => x.Questions).SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Exam> GetAllExams()
        {
            return _context.Exams.Include(x => x.Questions);
        }

        public ApiResponse<T> HttpGet<T>(string url)
        {
            return _client.GetAsync(url).GetApiResponse<T>();
        }

        public ApiResponse<T> HttpGet<T>(string url, Guid id)
        {
            return _client.GetAsync($"{url}/{id}").GetApiResponse<T>();
        }

        public ApiResponse<string> HttpPost(string url, object body)
        {
            return _client.PostAsync(url, body.ToStringContent()).GetApiResponse<string>();
        }

        public ApiResponse<string> HttpPut(string url, Guid id, object body)
        {
            return _client.PutAsync($"{url}/{id}", body.ToStringContent()).GetApiResponse<string>();
        }

        public ApiResponse<string> HttpDelete(string url, Guid id)
        {
            return _client.DeleteAsync($"{url}/{id}").GetApiResponse<string>();
        }

        public void Dispose()
        {
            _server?.Dispose();
            _client?.Dispose();
            _context?.Dispose();
        }
    }
}
