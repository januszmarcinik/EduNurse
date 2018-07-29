using System;
using System.Collections.Generic;
using System.Linq;
using EduNurse.Exams.Entities;
using EduNurse.Exams.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace EduNurse.Exams
{
    internal class EfCoreExamsRepository : IExamsRepository
    {
        private readonly ExamsContext _context;

        public EfCoreExamsRepository(ExamsContext context)
        {
            _context = context;
        }

        public Exam GetById(Guid id)
        {
            return _context.Exams
                .Include(p => p.Questions)
                .SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<string> GetCategoriesByType(ExamType type)
        {
            return _context.Exams
                .AsNoTracking()
                .AsQueryable()
                .Where(x => x.Type == type)
                .Select(x => x.Category)
                .Distinct()
                .ToList();
        }

        public IEnumerable<Exam> GetExamsByTypeAndCategory(ExamType type, string category)
        {
            return _context.Exams
                .AsNoTracking()
                .AsQueryable()
                .Where(x => x.Type == type)
                .Where(x => x.Category == category)
                .ToList();
        }

        public void Add(Exam exam)
        {
            _context.Add(exam);
            _context.SaveChanges();
        }

        public void Update(Exam exam)
        {
            _context.Update(exam);
            _context.SaveChanges();
        }

        public void Remove(Exam exam)
        {
            _context.Remove(exam);
            _context.SaveChanges();
        }
    }
}
