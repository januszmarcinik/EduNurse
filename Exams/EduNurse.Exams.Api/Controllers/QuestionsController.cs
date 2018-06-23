using System;
using EduNurse.Exams.Shared.Questions;
using Microsoft.AspNetCore.Mvc;

namespace EduNurse.Exams.Api.Controllers
{
    [Route("api/v1/questions")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionsRepository _questionsRepository;

        public QuestionsController(IQuestionsRepository questionsRepository)
        {
            _questionsRepository = questionsRepository;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_questionsRepository.GetAll());

        [HttpGet("{id}")]
        public IActionResult Get(Guid id) => Ok(_questionsRepository.GetById(id));

        [HttpPost]
        public IActionResult Post([FromBody] QuestionDto dto)
        {
            _questionsRepository.Create(dto);
            return Accepted();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] QuestionDto dto)
        {
            _questionsRepository.Update(id, dto);
            return Accepted();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _questionsRepository.Delete(id);
            return Accepted();
        }
    }
}
