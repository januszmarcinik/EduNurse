using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using EduNurse.Api.Shared;
using EduNurse.Api.Shared.Query;
using EduNurse.Auth.Extensions;
using EduNurse.Auth.Shared.Queries;
using EduNurse.Auth.Shared.Results;

namespace EduNurse.Auth.QueryHandlers
{
    internal class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, UserResult>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPrincipal _principal;

        public GetUserByEmailQueryHandler(IUsersRepository usersRepository, IPrincipal principal)
        {
            _usersRepository = usersRepository;
            _principal = principal;
        }

        public async Task<Result<UserResult>> HandleAsync(GetUserByEmailQuery query)
        {
            if (string.IsNullOrEmpty(query.Email))
            {
                return Result.Failure(query, "Email is required.");
            }

            if (!_principal.IsInRole(Role.GetUserByEmail) && query.Email != _principal.Identity.Name)
            {
                return Result.Failure(query, "Access denied.");
            }

            var user = await _usersRepository.GetByEmailAsync(query.Email);
            if (user == null)
            {
                return Result.Failure(query, "User with given email does not exists.");
            }

            var userRoles = user.Roles.Select(r => new UserResult.Role(r.ToString(), (int) r));
            var result = new UserResult(user.Email, user.IsAdmin, userRoles);

            return Result.Success(result);
        }
    }
}
