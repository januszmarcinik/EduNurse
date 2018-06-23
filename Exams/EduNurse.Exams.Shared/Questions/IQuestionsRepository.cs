using System;
using System.Collections.Generic;

namespace EduNurse.Exams.Shared.Questions
{
    public interface IQuestionsRepository
    {
        IEnumerable<QuestionDto> GetAll();
        QuestionDto GetById(Guid id);
        void Create(QuestionDto dto);
        void Update(Guid id, QuestionDto dto);
        void Delete(Guid id);
    }
}
