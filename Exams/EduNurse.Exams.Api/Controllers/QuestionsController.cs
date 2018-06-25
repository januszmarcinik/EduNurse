using System;
using EduNurse.Exams.Shared.Questions;
using Microsoft.AspNetCore.Mvc;

namespace EduNurse.Exams.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/questions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionsRepository _questionsRepository;

        public QuestionsController(IQuestionsRepository questionsRepository)
        {
            _questionsRepository = questionsRepository;
        }

        /// <summary>
        /// Get all questions
        /// </summary>
        /// <returns>All questions from service</returns>
        [HttpGet]
        public IActionResult Get() => Ok(_questionsRepository.GetAll());

        /// <summary>
        /// Get question by Id
        /// </summary>
        /// <param name="id">Question Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(Guid id) => Ok(_questionsRepository.GetById(id));

        /// <summary>
        /// Create new question
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / questions
        ///     {
        ///        "id": "1b7b385b-9fb4-45b9-800a-89392d0e866a",
        ///        "text": "Sample question text"
        ///     }
        ///
        /// </remarks>
        /// <param name="dto">model</param>
        /// <response code="202">New question is created</response>
        /// <response code="400">Validation failed</response>   
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        public IActionResult Post([FromBody] QuestionDto dto)
        {
            _questionsRepository.Create(dto);
            return Accepted();
        }

        /// <summary>
        /// Update exist question
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT / questions/1b7b385b-9fb4-45b9-800a-89392d0e866a
        ///     {
        ///        "id": "1b7b385b-9fb4-45b9-800a-89392d0e866a",
        ///        "text": "Sample question text"
        ///     }
        /// 
        /// </remarks>
        /// <param name="id">Id of exising question</param>
        /// <param name="dto">model</param>
        /// <response code="202">Question is updated</response>
        /// <response code="400">Validation failed</response>
        /// <response code="404">Question with given Id was not found</response> 
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Put(Guid id, [FromBody] QuestionDto dto)
        {
            _questionsRepository.Update(id, dto);
            return Accepted();
        }

        /// <summary>
        /// Delete existing question
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE / questions/1b7b385b-9fb4-45b9-800a-89392d0e866a
        /// 
        /// </remarks>
        /// <param name="id">Id of exising question</param>
        /// <response code="202">Question is deleted</response>
        /// <response code="404">Question with given Id was not found</response> 
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(404)]
        public IActionResult Delete(Guid id)
        {
            _questionsRepository.Delete(id);
            return Accepted();
        }
    }
}
