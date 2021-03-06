﻿using System;
using System.Threading.Tasks;
using EduNurse.Api.Shared.Command;
using EduNurse.Api.Shared.Query;
using EduNurse.Auth;
using EduNurse.Exams.Shared.Commands;
using EduNurse.Exams.Shared.Queries;
using Microsoft.AspNetCore.Mvc;

namespace EduNurse.Api.Controllers
{
    [Route("api/v1/exams")]
    public class ExamsController : ApiControllerBase
    {
        public ExamsController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
            : base(commandDispatcher, queryDispatcher)
        {
        }

        /// <summary>
        /// Get defined categories by exam type
        /// </summary>
        /// <response code="200">Distincted categories by given exam type</response>
        /// <returns>Distincted categories by given exam type</returns>
        [Auth(Role.GetCategoriesByType)]
        [HttpGet("{type}/categories")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get([FromRoute] GetCategoriesByTypeQuery query)
            => await DispatchQueryAsync(query);

        /// <summary>
        /// Get all exams by given parameters
        /// </summary>
        /// <response code="200">Found exams by given parameters</response>
        /// <returns>All exams by given parameters</returns>
        [Auth(Role.GetExamByTypeAndCategory)]
        [HttpGet("{type}/{category}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get([FromRoute] GetExamsByTypeAndCategoryQuery query)
            => await DispatchQueryAsync(query);

        /// <summary>
        /// Get exam by Id
        /// </summary>
        /// <returns></returns>
        [Auth(Role.GetExamById)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] GetExamByIdQuery query)
            => await DispatchQueryAsync(query);

        /// <summary>
        /// Create new exam
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="command">command</param>
        /// <response code="202">New exam is created</response>
        /// <response code="400">Validation failed</response>   
        /// <returns></returns>
        [Auth(Role.AddExam)]
        [HttpPost]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] AddExamCommand command)
            => await DispatchCommandAsync(command);

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
        [Auth(Role.EditExam)]
        [HttpPut("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(Guid id, [FromBody] EditExamCommand command)
            => await DispatchCommandAsync(command, id);

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
        [Auth(Role.DeleteExam)]
        [HttpDelete("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id, [FromRoute] DeleteExamCommand command)
            => await DispatchCommandAsync(command, id);
    }
}
