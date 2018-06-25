using System;
using System.Collections.Generic;
using System.Linq;
using EduNurse.Exams.Shared.Questions;

namespace EduNurse.Exams.Api.Questions
{
    internal class QuestionsRepository : IQuestionsRepository
    {
        private readonly IExamsContext _context;

        public QuestionsRepository(IExamsContext context)
        {
            _context = context;
        }

        public IEnumerable<QuestionDto> GetAll()
        {
            return _context.GetAll<Question>()
                .Select(x => new QuestionDto(x.Id, x.Text, x.A, x.B, x.C, x.D, x.CorrectAnswer))
                .ToList();
        }

        public QuestionDto GetById(Guid id)
        {
            var question = _context.GetById<Question>(id);
            return question == null
                ? null
                : new QuestionDto(question.Id, question.Text, question.A, question.B, question.C, question.D, question.CorrectAnswer);
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
