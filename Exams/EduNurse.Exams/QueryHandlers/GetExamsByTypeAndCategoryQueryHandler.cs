using System.Collections.Generic;
using AutoMapper;
using EduNurse.Api.Shared.Query;
using EduNurse.Exams.Shared.Queries;
using EduNurse.Exams.Shared.Results;

namespace EduNurse.Exams.QueryHandlers
{
    internal class GetExamsByTypeAndCategoryQueryHandler : IQueryHandler<GetExamsByTypeAndCategoryQuery, ExamsResult>
    {
        private readonly IExamsRepository _examsRepository;
        private readonly IMapper _mapper;

        public GetExamsByTypeAndCategoryQueryHandler(IExamsRepository examsRepository, IMapper mapper)
        {
            _examsRepository = examsRepository;
            _mapper = mapper;
        }

        public ExamsResult Handle(GetExamsByTypeAndCategoryQuery query)
        {
            var exams = _examsRepository.GetExamsByTypeAndCategory(query.Type, query.Category);
            var result = _mapper.Map<IEnumerable<ExamsResult.Exam>>(exams);

            return new ExamsResult(result);
        }
    }
}
