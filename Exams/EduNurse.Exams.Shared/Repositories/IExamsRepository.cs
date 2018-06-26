using System;
using System.Collections.Generic;
using EduNurse.Exams.Shared.Dto;

namespace EduNurse.Exams.Shared.Repositories
{
    public interface IExamsRepository
    {
        IEnumerable<ExamDto> GetAll();
        ExamDto GetById(Guid id);
        void Create(ExamDto dto);
        void Update(Guid id, ExamDto dto);
        void Delete(Guid id);
    }
}
