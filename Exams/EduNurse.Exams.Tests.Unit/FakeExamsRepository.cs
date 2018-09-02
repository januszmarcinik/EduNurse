using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduNurse.Exams.Entities;
using EduNurse.Exams.Shared.Enums;
using EduNurse.Exams.Tests.Unit.Extensions;

namespace EduNurse.Exams.Tests.Unit
{
    internal class FakeExamsRepository : IExamsRepository
    {
        private readonly List<Exam> _exams = new List<Exam>();
        public IReadOnlyCollection<Exam> Exams => _exams;

        public async Task<Exam> GetByIdAsync(Guid id)
        {
            var exam = _exams.SingleOrDefault(x => x.Id == id);
            return await Task.FromResult(exam);
        }

        public async Task<IEnumerable<string>> GetCategoriesByTypeAsync(ExamType type)
        {
            var categories = _exams
                .Where(x => x.Type == type)
                .Select(x => x.Category)
                .Distinct()
                .ToList();

            return await Task.FromResult(categories);
        }

        public async Task<IEnumerable<Exam>> GetExamsByTypeAndCategoryAsync(ExamType type, string category)
        {
            var exams = _exams
                .Where(x => x.Type == type)
                .Where(x => x.Category == category)
                .ToList();

            return await Task.FromResult(exams);
        }

        public async Task AddAsync(Exam exam)
        {
            _exams.Add(exam);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Exam exam)
        {
            await RemoveAsync(exam);
            await AddAsync(exam.DeepClone());
        }

        public async Task RemoveAsync(Exam exam)
        {
            _exams.RemoveAll(x => x.Id == exam.Id);
            await Task.CompletedTask;
        }
    }
}
