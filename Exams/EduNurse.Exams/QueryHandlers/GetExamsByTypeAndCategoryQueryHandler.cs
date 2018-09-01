using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EduNurse.Api.Shared;
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

        public async Task<Result<ExamsResult>> HandleAsync(GetExamsByTypeAndCategoryQuery query)
        {
            var exams = await _examsRepository.GetExamsByTypeAndCategoryAsync(query.Type, query.Category);
            var result = _mapper.Map<IEnumerable<ExamsResult.Exam>>(exams);

            return Result.Success(new ExamsResult(result));
        }
    }
}
