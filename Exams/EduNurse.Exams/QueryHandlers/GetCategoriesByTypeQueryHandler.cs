using System.Linq;
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

        public CategoriesResult Handle(GetCategoriesByTypeQuery query)
        {
            var categories = _examsRepository.GetCategoriesByType(query.Type)
                .Select(x => new CategoriesResult.Category(x))
                .ToList();

            return new CategoriesResult(categories);
        }
    }
}
