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
    }
}
