using System;
using System.Threading.Tasks;
using EduNurse.Api.Shared.Command;
using EduNurse.Api.Shared.Query;
using Microsoft.AspNetCore.Mvc;

namespace EduNurse.Api
{
    [Produces("application/json")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase 
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        protected ApiControllerBase(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        protected async Task<IActionResult> DispatchCommandAsync<T>(T command) where T : ICommand
        {
            var result = await _commandDispatcher.DispatchAsync(command, User);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Accepted(result);
        }

        protected async Task<IActionResult> DispatchCommandAsync<T>(T command, Guid objectId) where T : ICommand
        {
            var result = await _commandDispatcher.DispatchAsync(command, User, objectId);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Accepted(result);
        }

        protected async Task<IActionResult> DispatchQueryAsync<T>(IQuery<T> query)
        {
            var result = await _queryDispatcher.DispatchAsync(query);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
