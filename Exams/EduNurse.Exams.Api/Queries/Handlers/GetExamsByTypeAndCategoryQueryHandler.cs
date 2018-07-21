using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EduNurse.Exams.Shared.Queries;
using EduNurse.Exams.Shared.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduNurse.Exams.Api.Queries.Handlers
{
    internal class GetExamsByTypeAndCategoryQueryHandler : IQueryHandler<GetExamsByTypeAndCategoryQuery>
    {
        private readonly ExamsContext _context;
        private readonly IMapper _mapper;

        public GetExamsByTypeAndCategoryQueryHandler(ExamsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Handle(GetExamsByTypeAndCategoryQuery query)
        {
            var exams = _context.Exams.AsQueryable()
                .Where(x => x.Type == query.Type)
                .Where(x => x.Category == query.Category)
                .ToList();

            return new OkObjectResult(_mapper.Map<IEnumerable<ExamResult>>(exams));
        }
    }
}
