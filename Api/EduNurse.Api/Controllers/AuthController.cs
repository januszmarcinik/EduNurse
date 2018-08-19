using System.Threading.Tasks;
using EduNurse.Api.Shared.Command;
using EduNurse.Auth.Shared.Commands;
using Microsoft.AspNetCore.Mvc;

namespace EduNurse.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public AuthController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
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
        {
            await _commandDispatcher.DispatchAsync(command, User);
            return Accepted();
        }
    }
}
