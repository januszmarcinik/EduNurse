using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EduNurse.Api.Shared.Query;
using EduNurse.Exams.Shared.Queries;
using EduNurse.Exams.Shared.Results;

namespace EduNurse.Exams.QueryHandlers
{
    internal class GetExamsByTypeAndCategoryQueryHandler : IQueryHandler<GetExamsByTypeAndCategoryQuery, ExamsResult>
    {
        private readonly ExamsContext _context;
        private readonly IMapper _mapper;

        public GetExamsByTypeAndCategoryQueryHandler(ExamsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ExamsResult Handle(GetExamsByTypeAndCategoryQuery query)
        {
            var exams = _context.Exams.AsQueryable()
                .Where(x => x.Type == query.Type)
                .Where(x => x.Category == query.Category)
                .ToList();

            var result = _mapper.Map<IEnumerable<ExamsResult.Exam>>(exams);

            return new ExamsResult(result);
        }
    }
}
