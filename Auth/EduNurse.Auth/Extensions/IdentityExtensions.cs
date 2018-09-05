using System.Security.Principal;

namespace EduNurse.Auth.Extensions
{
    public static class IdentityExtensions
    {
        public static bool IsInRole(this IPrincipal principal, Role role)
        {
            return principal.IsInRole(role.ToString());
        }
    }
}
