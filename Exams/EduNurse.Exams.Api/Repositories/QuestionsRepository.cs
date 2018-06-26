using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EduNurse.Exams.Api.Entities;
using EduNurse.Exams.Shared.Dto;
using EduNurse.Exams.Shared.Repositories;

namespace EduNurse.Exams.Api.Repositories
{
    internal class QuestionsRepository : IQuestionsRepository
    {
        private readonly IExamsContext _context;
        private readonly IMapper _mapper;

        public QuestionsRepository(IExamsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<QuestionDto> GetAll()
        {
            var questions = _context.GetAll<Question>().ToList();
            return _mapper.Map<IEnumerable<QuestionDto>>(questions);
        }

        public QuestionDto GetById(Guid id)
        {
            var question = _context.GetById<Question>(id);
            return question == null
                ? null
                : _mapper.Map<QuestionDto>(question);
        }

        public void Create(QuestionDto dto)
        {
            var question = new Question(dto.Id, dto.Text, dto.A, dto.B, dto.C, dto.D, dto.CorrectAnswer);
            _context.Create(question);
            _context.SaveChanges();
        }

        public void Update(Guid id, QuestionDto dto)
        {
            var question = new Question(id, dto.Text, dto.A, dto.B, dto.C, dto.D, dto.CorrectAnswer);
            _context.Update(question);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _context.Delete<Question>(id);
            _context.SaveChanges();
        }
    }
}
