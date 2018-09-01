using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EduNurse.Api.Shared;
using EduNurse.Api.Shared.Query;
using EduNurse.Exams.Shared.Queries;
using EduNurse.Exams.Shared.Results;

namespace EduNurse.Exams.QueryHandlers
{
    internal class GetExamByIdQueryHandler : IQueryHandler<GetExamByIdQuery, ExamWithQuestionsResult>
    {
        private readonly IExamsRepository _examsRepository;
        private readonly IMapper _mapper;

        public GetExamByIdQueryHandler(IExamsRepository examsRepository, IMapper mapper)
        {
            _examsRepository = examsRepository;
            _mapper = mapper;
        }

        public async Task<Result<ExamWithQuestionsResult>> HandleAsync(GetExamByIdQuery query)
        {
            var exam = await _examsRepository.GetByIdAsync(query.Id);
            if (exam == null)
            {
                return Result.Failure(query, "Exam with given Id does not exists.");
            }

            var result = _mapper.Map<ExamWithQuestionsResult>(exam);
            result.Questions = result.Questions.OrderBy(x => x.Order).ToList();

            return Result.Success(result);
        }
    }
}
