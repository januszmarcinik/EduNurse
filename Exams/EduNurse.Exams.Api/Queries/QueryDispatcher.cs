using System.Collections.Generic;
using System.Security.Principal;
using Autofac;
using Autofac.Core;
using EduNurse.Exams.Shared;
using Microsoft.AspNetCore.Mvc;

namespace EduNurse.Exams.Api.Queries
{
    internal class QueryDispatcher : IQueryDispatcher
    {
        private readonly IComponentContext _context;

        public QueryDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public IActionResult Dispatch<TQuery>(TQuery query) where TQuery : IQuery
        {
            return Dispatch<TQuery>(query, null);
        }

        public IActionResult Dispatch<TQuery>(TQuery query, IPrincipal user) where TQuery : IQuery
        {
            var parameters = new List<Parameter>();

            if (user != null)
            {
                parameters.Add(new TypedParameter(typeof(IPrincipal), user));
            }

            var handler = _context.Resolve<IQueryHandler<TQuery>>(parameters);
            return handler.Handle(query);
        }
    }
}
