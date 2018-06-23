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
                .Select(x => new QuestionDto(x.Id, x.Text))
                .ToList();
        }

        public QuestionDto GetById(Guid id)
        {
            var question = _context.GetById<Question>(id);
            return new QuestionDto(question.Id, question.Text);
        }
    }
}
