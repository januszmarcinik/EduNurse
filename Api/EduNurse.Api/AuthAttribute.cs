using System;
using System.Linq;
using EduNurse.Auth;
using EduNurse.Auth.Shared;
using Microsoft.AspNetCore.Authorization;

namespace EduNurse.Api
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    internal class AuthAttribute : Attribute, IAuthorizeData
    {
        public AuthAttribute(params Role[] roles)
        {
            Roles = string.Join(",", roles.Select(r => r.ToString()));
        }

        public string Policy { get; set; }
        public string Roles { get; set; }
        public string AuthenticationSchemes { get; set; }
    }
}
