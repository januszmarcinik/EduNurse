using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Exam> GetByIdAsync(Guid id)
        {
            return await _context.Exams
                .Include(p => p.Questions)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<string>> GetCategoriesByTypeAsync(ExamType type)
        {
            return await _context.Exams
                .AsNoTracking()
                .AsQueryable()
                .Where(x => x.Type == type)
                .Select(x => x.Category)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<Exam>> GetExamsByTypeAndCategoryAsync(ExamType type, string category)
        {
            return await _context.Exams
                .AsNoTracking()
                .AsQueryable()
                .Where(x => x.Type == type)
                .Where(x => x.Category == category)
                .ToListAsync();
        }

        public async Task AddAsync(Exam exam)
        {
            await _context.AddAsync(exam);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Exam exam)
        {
            _context.Update(exam);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Exam exam)
        {
            _context.Remove(exam);
            await _context.SaveChangesAsync();
        }
    }
}
