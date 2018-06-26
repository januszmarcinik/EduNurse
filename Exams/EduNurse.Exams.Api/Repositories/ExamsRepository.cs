using System;
using System.Collections.Generic;
using System.Linq;
using EduNurse.Exams.Api.Entities;
using EduNurse.Exams.Shared.Dto;
using EduNurse.Exams.Shared.Repositories;

namespace EduNurse.Exams.Api.Repositories
{
    internal class ExamsRepository : IExamsRepository
    {
        private readonly IExamsContext _context;

        public ExamsRepository(IExamsContext context)
        {
            _context = context;
        }

        public IEnumerable<ExamDto> GetAll()
        {
            return _context.GetAll<Exam>()
                .Select(x => new ExamDto(x.Id, x.Name, x.Type, x.Category))
                .ToList();
        }

        public ExamDto GetById(Guid id)
        {
            var exam = _context.GetById<Exam>(id);
            return exam == null
                ? null
                : new ExamDto(exam.Id, exam.Name, exam.Type, exam.Category);
        }

        public void Create(ExamDto dto)
        {
            var exam = new Exam(dto.Id, dto.Name, dto.Type, dto.Category);
            _context.Create(exam);
            _context.SaveChanges();
        }

        public void Update(Guid id, ExamDto dto)
        {
            var exam = new Exam(dto.Id, dto.Name, dto.Type, dto.Category);
            _context.Update(exam);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _context.Delete<Exam>(id);
            _context.SaveChanges();
        }
    }
}
