using System.Linq;
using EduNurse.Exams.Shared.Queries;
using EduNurse.Exams.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace EduNurse.Exams.Api.Queries.Handlers
{
    internal class GetCategoriesByTypeQueryHandler : IQueryHandler<GetCategoriesByTypeQuery>
    {
        private readonly ExamsContext _context;

        public GetCategoriesByTypeQueryHandler(ExamsContext context)
        {
            _context = context;
        }

        public IActionResult Handle(GetCategoriesByTypeQuery query)
        {
            var categories = _context.Exams
                .AsQueryable()
                .Where(x => x.Type == query.Type)
                .Select(x => x.Category)
                .Distinct()
                .Select(x => new CategoryResult(x))
                .ToList();

            return new OkObjectResult(categories);
        }
    }
}
