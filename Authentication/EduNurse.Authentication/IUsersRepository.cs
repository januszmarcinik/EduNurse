using System.Threading.Tasks;
using EduNurse.Authentication.Entities;

namespace EduNurse.Authentication
{
    internal interface IUsersRepository
    {
        Task<User> GetByEmailAsync(string email);

        Task AddAsync(User user);
    }
}
