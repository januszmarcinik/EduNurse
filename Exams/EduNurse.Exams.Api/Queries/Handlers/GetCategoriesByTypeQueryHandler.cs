using System.Linq;
using EduNurse.Api.Shared.Query;
using EduNurse.Exams.Shared.Queries;
using EduNurse.Exams.Shared.Results;

namespace EduNurse.Exams.Api.Queries.Handlers
{
    internal class GetCategoriesByTypeQueryHandler : IQueryHandler<GetCategoriesByTypeQuery, CategoriesResult>
    {
        private readonly ExamsContext _context;

        public GetCategoriesByTypeQueryHandler(ExamsContext context)
        {
            _context = context;
        }

        public CategoriesResult Handle(GetCategoriesByTypeQuery query)
        {
            var categories = _context.Exams
                .AsQueryable()
                .Where(x => x.Type == query.Type)
                .Select(x => x.Category)
                .Distinct()
                .Select(x => new CategoriesResult.Category(x))
                .ToList();

            return new CategoriesResult(categories);
        }
    }
}
