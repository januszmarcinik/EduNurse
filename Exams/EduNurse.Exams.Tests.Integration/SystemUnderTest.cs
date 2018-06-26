using System;
using System.Collections.Generic;
using System.Net.Http;
using EduNurse.Exams.Api;
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

        public T Create<T>(T entity) where T : Entity
        {
            _context.Create(entity);
            _context.SaveChanges();

            return entity;
        }

        public List<T> CreateMany<T>(List<T> entities) where T : Entity
        {
            _context.CreateMany(entities);
            _context.SaveChanges();

            return entities;
        }

        public T GetById<T>(Guid id) where T : Entity
        {
            return _context.GetById<T>(id);
        }

        public ApiResponse HttpGet(string url)
        {
            return _client.GetAsync(url).GetApiResponse();
        }

        public ApiResponse HttpGet(string url, Guid id)
        {
            return _client.GetAsync($"{url}/{id}").GetApiResponse();
        }

        public ApiResponse HttpPost(string url, object body)
        {
            return _client.PostAsync(url, body.ToStringContent()).GetApiResponse();
        }

        public ApiResponse HttpPut(string url, Guid id, object body)
        {
            return _client.PutAsync($"{url}/{id}", body.ToStringContent()).GetApiResponse();
        }

        public ApiResponse HttpDelete(string url, Guid id)
        {
            return _client.DeleteAsync($"{url}/{id}").GetApiResponse();
        }

        public void Dispose()
        {
            _server?.Dispose();
            _client?.Dispose();
            _context?.Dispose();
        }
    }
}
