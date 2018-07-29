using System;
using System.Collections.Generic;
using EduNurse.Exams.Entities;
using EduNurse.Exams.Shared.Enums;

namespace EduNurse.Exams
{
    internal interface IExamsRepository
    {
        Exam GetById(Guid id);
        IEnumerable<string> GetCategoriesByType(ExamType type);
        IEnumerable<Exam> GetExamsByTypeAndCategory(ExamType type, string category);

        void Add(Exam exam);
        void Update(Exam exam);
        void Remove(Exam exam);
    }
}
