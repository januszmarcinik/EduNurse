using System;
using EduNurse.Exams.Shared.Dto;
using EduNurse.Exams.Shared.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EduNurse.Exams.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/exams")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamsRepository _examsRepository;

        public ExamsController(IExamsRepository examsRepository)
        {
            _examsRepository = examsRepository;
        }

        /// <summary>
        /// Get all exams
        /// </summary>
        /// <returns>All exams from service</returns>
        [HttpGet]
        public IActionResult Get() => Ok(_examsRepository.GetAll());

        /// <summary>
        /// Get exam by Id
        /// </summary>
        /// <param name="id">Exam Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id) => Ok(_examsRepository.GetById(id));

        /// <summary>
        /// Create new exam
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / exams
        ///     {
        ///        "id": "1b7b385b-9fb4-45b9-800a-89392d0e866a",
        ///        "name": "Sample exam name"
        ///     }
        ///
        /// </remarks>
        /// <param name="dto">model</param>
        /// <response code="202">New exam is created</response>
        /// <response code="400">Validation failed</response>   
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] ExamDto dto)
        {
            _examsRepository.Create(dto);
            return Accepted();
        }

        /// <summary>
        /// Update exist exam
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT / exams/1b7b385b-9fb4-45b9-800a-89392d0e866a
        ///     {
        ///        "id": "1b7b385b-9fb4-45b9-800a-89392d0e866a",
        ///        "name": "Sample exam name"
        ///     }
        /// 
        /// </remarks>
        /// <param name="id">Id of exising exam</param>
        /// <param name="dto">model</param>
        /// <response code="202">Exam is updated</response>
        /// <response code="400">Validation failed</response>
        /// <response code="404">Exam with given Id was not found</response> 
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(Guid id, [FromBody] ExamDto dto)
        {
            _examsRepository.Update(id, dto);
            return Accepted();
        }

        /// <summary>
        /// Delete existing exam
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE / exams/1b7b385b-9fb4-45b9-800a-89392d0e866a
        /// 
        /// </remarks>
        /// <param name="id">Id of exising exam</param>
        /// <response code="202">Exam is deleted</response>
        /// <response code="404">Exam with given Id was not found</response> 
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(404)]
        public IActionResult Delete(Guid id)
        {
            _examsRepository.Delete(id);
            return Accepted();
        }
    }
}
