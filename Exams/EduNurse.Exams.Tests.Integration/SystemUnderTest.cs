using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using EduNurse.Exams.Entities;
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

        public SystemUnderTest()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseSetting("ConnectionStrings:MSSQL", Guid.NewGuid().ToString())
                .UseStartup<Api.Startup>()
            );
            _client = _server.CreateClient();
        }

        public T Create<T>(T entity) where T : Entity
        {
            using (var scope = _server.Host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ExamsContext>();
                context.Add(entity);
                context.SaveChanges();
            }

            return entity;
        }

        public List<T> CreateMany<T>(List<T> entities) where T : Entity
        {
            using (var scope = _server.Host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ExamsContext>();
                context.AddRange(entities);
                context.SaveChanges();
            }

            return entities;
        }

        public Exam GetExamById(Guid id)
        {
            using (var scope = _server.Host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ExamsContext>();
                return context.Exams.Include(x => x.Questions).SingleOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<Exam> GetAllExams()
        {
            using (var scope = _server.Host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ExamsContext>();
                return context.Exams.Include(x => x.Questions).ToList();
            }
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
        }
    }
}
