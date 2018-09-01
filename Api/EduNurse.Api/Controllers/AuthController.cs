﻿using System.Threading.Tasks;
using EduNurse.Api.Shared.Command;
using EduNurse.Api.Shared.Query;
using EduNurse.Auth.Shared.Commands;
using Microsoft.AspNetCore.Mvc;

namespace EduNurse.Api.Controllers
{
    [Route("api/v1/auth")]
    public class AuthController : ApiControllerBase
    {
        public AuthController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
            : base(commandDispatcher, queryDispatcher)
        {
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="command">command</param>
        /// <response code="202">New user is created</response>
        /// <response code="400">Validation failed</response>   
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] RegisterCommand command)
            => await DispatchCommandAsync(command);
    }
}
