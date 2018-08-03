using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EduNurse.Exams.Entities;
using EduNurse.Exams.Shared.Enums;

namespace EduNurse.Exams
{
    internal interface IExamsRepository
    {
        Task<Exam> GetByIdAsync(Guid id);
        Task<IEnumerable<string>> GetCategoriesByTypeAsync(ExamType type);
        Task<IEnumerable<Exam>> GetExamsByTypeAndCategoryAsync(ExamType type, string category);

        Task AddAsync(Exam exam);
        Task UpdateAsync(Exam exam);
        Task RemoveAsync(Exam exam);
    }
}
