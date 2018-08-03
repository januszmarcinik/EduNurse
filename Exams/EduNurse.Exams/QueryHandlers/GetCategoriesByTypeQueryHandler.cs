using System.Linq;
using System.Threading.Tasks;
using EduNurse.Api.Shared.Query;
using EduNurse.Exams.Shared.Queries;
using EduNurse.Exams.Shared.Results;

namespace EduNurse.Exams.QueryHandlers
{
    internal class GetCategoriesByTypeQueryHandler : IQueryHandler<GetCategoriesByTypeQuery, CategoriesResult>
    {
        private readonly IExamsRepository _examsRepository;

        public GetCategoriesByTypeQueryHandler(IExamsRepository examsRepository)
        {
            _examsRepository = examsRepository;
        }

        public async Task<CategoriesResult> HandleAsync(GetCategoriesByTypeQuery query)
        {
            var categories = (await _examsRepository.GetCategoriesByTypeAsync(query.Type))
                .Select(x => new CategoriesResult.Category(x))
                .ToList();

            return new CategoriesResult(categories);
        }
    }
}
