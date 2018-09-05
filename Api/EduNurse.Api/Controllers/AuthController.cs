using System.Threading.Tasks;
using EduNurse.Api.Shared.Command;
using EduNurse.Api.Shared.Query;
using EduNurse.Auth.Shared.Commands;
using EduNurse.Auth.Shared.Queries;
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
        /// Get user by email
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="query">query</param>
        /// <response code="200">User result</response>
        /// <response code="400">Validation failed or access denied</response>   
        /// <returns></returns>
        [HttpGet("{email}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Get([FromRoute] GetUserByEmailQuery query)
            => await DispatchQueryAsync(query);

        /// <summary>
        /// Register new user
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="command">command</param>
        /// <response code="202">New user is created</response>
        /// <response code="400">Validation failed</response>   
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
            => await DispatchCommandAsync(command);

        /// <summary>
        /// Sign in user
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="command">command</param>
        /// <response code="202">User is authenticated</response>
        /// <response code="400">User is not authenticated</response>   
        /// <returns></returns>
        [HttpPost("sign-in")]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SignIn([FromBody] SignInCommand command)
            => await DispatchCommandAsync(command);
    }
}
