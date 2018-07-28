using System.Collections.Generic;
using System.Security.Principal;
using Autofac;
using Autofac.Core;
using EduNurse.Api.Shared.Query;
using EduNurse.Api.Shared.Result;
using EduNurse.Exams.Shared;

namespace EduNurse.Exams.Api.Queries
{
    internal class QueryDispatcher : IQueryDispatcher
    {
        private readonly IComponentContext _context;

        public QueryDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public TResult Dispatch<TQuery, TResult>(TQuery query)
            where TQuery : IQuery<TResult>
            where TResult : IResult
        {
            return Dispatch<TQuery, TResult>(query, null);
        }

        public TResult Dispatch<TQuery, TResult>(TQuery query, IPrincipal user)
            where TQuery : IQuery<TResult>
            where TResult : IResult
        {
            var parameters = new List<Parameter>();

            if (user != null)
            {
                parameters.Add(new TypedParameter(typeof(IPrincipal), user));
            }

            var handler = _context.Resolve<IQueryHandler<TQuery, TResult>>(parameters);
            return handler.Handle(query);
        }
    }
}
