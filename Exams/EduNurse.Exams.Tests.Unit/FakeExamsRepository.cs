using System;
using System.Collections.Generic;
using System.Linq;
using EduNurse.Exams.Entities;
using EduNurse.Exams.Shared.Enums;
using EduNurse.Exams.Tests.Shared.Extensions;

namespace EduNurse.Exams.Tests.Unit
{
    internal class FakeExamsRepository : IExamsRepository
    {
        private readonly List<Exam> _exams = new List<Exam>();
        public IReadOnlyCollection<Exam> Exams => _exams;

        public Exam GetById(Guid id)
        {
            return _exams.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<string> GetCategoriesByType(ExamType type)
        {
            return _exams
                .Where(x => x.Type == type)
                .Select(x => x.Category)
                .Distinct()
                .ToList();
        }

        public IEnumerable<Exam> GetExamsByTypeAndCategory(ExamType type, string category)
        {
            return _exams
                .Where(x => x.Type == type)
                .Where(x => x.Category == category)
                .ToList();
        }

        public void Add(Exam exam)
        {
            _exams.Add(exam);
        }

        public void Update(Exam exam)
        {
            Remove(exam);
            Add(exam.DeepClone());
        }

        public void Remove(Exam exam)
        {
            _exams.RemoveAll(x => x.Id == exam.Id);
        }
    }
}
