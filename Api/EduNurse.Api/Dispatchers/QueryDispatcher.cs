using System.Collections.Generic;
using System.Security.Principal;
using Autofac;
using Autofac.Core;
using EduNurse.Api.Shared.Query;

namespace EduNurse.Api.Dispatchers
{
    internal class QueryDispatcher : IQueryDispatcher
    {
        private readonly IComponentContext _context;

        public QueryDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public TResult Dispatch<TResult>(IQuery<TResult> query)
        {
            return Dispatch(query, null);
        }

        public TResult Dispatch<TResult>(IQuery<TResult> query, IPrincipal user)
        {
            var parameters = new List<Parameter>();

            if (user != null)
            {
                parameters.Add(new TypedParameter(typeof(IPrincipal), user));
            }

            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            var handler = _context.Resolve(handlerType, parameters);

            var handleMethod = handlerType.GetMethod("Handle");
            return (TResult)handleMethod.Invoke(handler, new object[] { query });
        }
    }
}
