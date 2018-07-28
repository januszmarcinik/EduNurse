﻿using System.Linq;
using AutoMapper;
using EduNurse.Api.Shared.Query;
using EduNurse.Exams.Shared.Queries;
using EduNurse.Exams.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace EduNurse.Exams.Api.Queries.Handlers
{
    internal class GetExamByIdQueryHandler : IQueryHandler<GetExamByIdQuery, ExamWithQuestionsResult>
    {
        private readonly ExamsContext _context;
        private readonly IMapper _mapper;

        public GetExamByIdQueryHandler(ExamsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ExamWithQuestionsResult Handle(GetExamByIdQuery query)
        {
            var exam = _context.Exams
                .Include(x => x.Questions)
                .SingleOrDefault(x => x.Id == query.Id);

            if (exam == null)
            {
                return null;
            }

            var result = _mapper.Map<ExamWithQuestionsResult>(exam);
            result.Questions = result.Questions.OrderBy(x => x.Order).ToList();

            return result;
        }
    }
}
