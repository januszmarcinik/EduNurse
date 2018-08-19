using System.Threading.Tasks;
using EduNurse.Auth.Entities;

namespace EduNurse.Auth
{
    internal interface IUsersRepository
    {
        Task<User> GetByEmailAsync(string email);

        Task AddAsync(User user);
    }
}
