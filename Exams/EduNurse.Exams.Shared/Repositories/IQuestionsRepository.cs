using System;
using System.Collections.Generic;
using EduNurse.Exams.Shared.Dto;

namespace EduNurse.Exams.Shared.Repositories
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
