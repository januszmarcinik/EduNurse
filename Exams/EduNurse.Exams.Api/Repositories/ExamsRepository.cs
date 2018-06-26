using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EduNurse.Exams.Api.Entities;
using EduNurse.Exams.Shared.Dto;
using EduNurse.Exams.Shared.Repositories;

namespace EduNurse.Exams.Api.Repositories
{
    internal class ExamsRepository : IExamsRepository
    {
        private readonly IExamsContext _context;
        private readonly IMapper _mapper;

        public ExamsRepository(IExamsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<ExamDto> GetAll()
        {
            var exams = _context.GetAll<Exam>().ToList();
            return _mapper.Map<IEnumerable<ExamDto>>(exams);
        }

        public ExamDto GetById(Guid id)
        {
            var exam = _context.GetById<Exam>(id);
            return exam == null
                ? null
                : _mapper.Map<ExamDto>(exam);
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
