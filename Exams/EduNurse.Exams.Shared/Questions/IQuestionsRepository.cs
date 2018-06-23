using System;
using System.Collections.Generic;

namespace EduNurse.Exams.Shared.Questions
{
    public interface IQuestionsRepository
    {
        IEnumerable<QuestionDto> GetAll();
        QuestionDto GetById(Guid id);
    }
}
