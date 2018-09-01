using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using EduNurse.Api.Shared;
using EduNurse.Api.Shared.Command;

namespace EduNurse.Api.Dispatchers
{
    internal class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public async Task<Result> DispatchAsync<T>(T command) where T : ICommand
        {
            return await DispatchAsync(command, null, Guid.Empty);
        }

        public async Task<Result> DispatchAsync<T>(T command, IPrincipal user) where T : ICommand
        {
            return await DispatchAsync(command, user, Guid.Empty);
        }

        public async Task<Result> DispatchAsync<T>(T command, Guid objectId) where T : ICommand
        {
            return await DispatchAsync(command, null, objectId);
        }

        public async Task<Result> DispatchAsync<T>(T command, IPrincipal user, Guid objectId) where T : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException($"Command of type '{typeof(T).Name}' is null.");
            }

            var parameters = new List<Parameter>();

            if (user != null)
            {
                parameters.Add(new TypedParameter(typeof(IPrincipal), user));
            }

            if (objectId != Guid.Empty)
            {
                parameters.Add(new TypedParameter(typeof(Guid), objectId));
            }

            var handler = _context.Resolve<ICommandHandler<T>>(parameters);
            return await handler.HandleAsync(command);
        }
    }
}
