using EduNurse.Api.Shared.Query;
using EduNurse.Auth.Shared.Results;

namespace EduNurse.Auth.Shared.Queries
{
    public class GetUserByEmailQuery : IQuery<UserResult>
    {
        public string Email { get; set; }
    }
}
