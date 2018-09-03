using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduNurse.Auth.Entities;

namespace EduNurse.Auth.Tests.Unit.Fakes
{
    internal class FakeUsersRepository : IUsersRepository
    {
        private readonly List<User> _users = new List<User>();

        public async Task<bool> CheckIfExistsAsync(string email)
        {
            return await Task.FromResult(_users.SingleOrDefault(x => x.Email == email) != null);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await Task.FromResult(_users.SingleOrDefault(x => x.Email == email));
        }

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }
    }
}
