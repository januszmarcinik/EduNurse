using System.Linq;
using EduNurse.Exams.Shared.Queries;
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
                .ToList();

            return new OkObjectResult(categories);
        }
    }
}
