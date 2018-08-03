using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
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

        public async Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query)
        {
            return await DispatchAsync(query, null);
        }

        public Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query, IPrincipal user)
        {
            var parameters = new List<Parameter>();

            if (user != null)
            {
                parameters.Add(new TypedParameter(typeof(IPrincipal), user));
            }

            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            var handler = _context.Resolve(handlerType, parameters);

            var handleMethod = handlerType.GetMethod("HandleAsync");
            var task = (Task<TResult>)handleMethod.Invoke(handler, new object[] { query });
            task.Wait();

            return task;
        }
    }
}
