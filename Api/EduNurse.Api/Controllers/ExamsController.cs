using System;
using System.Threading.Tasks;
using EduNurse.Api.Shared.Command;
using EduNurse.Api.Shared.Query;
using EduNurse.Exams.Shared.Commands;
using EduNurse.Exams.Shared.Queries;
using Microsoft.AspNetCore.Mvc;

namespace EduNurse.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/exams")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public ExamsController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        /// <summary>
        /// Get defined categories by exam type
        /// </summary>
        /// <response code="200">Distincted categories by given exam type</response>
        /// <returns>Distincted categories by given exam type</returns>
        [HttpGet("{type}/categories")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get([FromRoute] GetCategoriesByTypeQuery query)
            => Ok(await _queryDispatcher.DispatchAsync(query));

        /// <summary>
        /// Get all exams by given parameters
        /// </summary>
        /// <response code="200">Found exams by given parameters</response>
        /// <returns>All exams by given parameters</returns>
        [HttpGet("{type}/{category}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get([FromRoute] GetExamsByTypeAndCategoryQuery query) 
            => Ok(await _queryDispatcher.DispatchAsync(query));

        /// <summary>
        /// Get exam by Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] GetExamByIdQuery query)
            => Ok(await _queryDispatcher.DispatchAsync(query));

        /// <summary>
        /// Create new exam
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="command">command</param>
        /// <response code="202">New exam is created</response>
        /// <response code="400">Validation failed</response>   
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] AddExamCommand command)
        {
            await _commandDispatcher.DispatchAsync(command, User);
            return Accepted();
        }

        /// <summary>
        /// Update exist exam
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Id of exising exam</param>
        /// <param name="command">command</param>
        /// <response code="202">Exam is updated</response>
        /// <response code="400">Validation failed</response>
        /// <response code="404">Exam with given Id was not found</response> 
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(Guid id, [FromBody] EditExamCommand command)
        {
            await _commandDispatcher.DispatchAsync(command, User, id);
            return Accepted();
        }

        /// <summary>
        /// Remove existing exam
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="id">Id of exising exam</param>
        /// <param name="command"></param>
        /// <response code="202">Exam is deleted</response>
        /// <response code="404">Exam with given Id was not found</response> 
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id, [FromRoute] DeleteExamCommand command)
        {
            await _commandDispatcher.DispatchAsync(command, User, id);
            return Accepted();
        }
    }
}
