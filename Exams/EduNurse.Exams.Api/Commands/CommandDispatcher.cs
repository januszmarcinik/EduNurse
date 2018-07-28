using System;
using System.Collections.Generic;
using System.Security.Principal;
using Autofac;
using Autofac.Core;
using EduNurse.Exams.Shared;

namespace EduNurse.Exams.Api.Commands
{
    internal class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public void Dispatch<T>(T command) where T : ICommand
        {
            Dispatch(command, null, Guid.Empty);
        }

        public void Dispatch<T>(T command, IPrincipal user) where T : ICommand
        {
            Dispatch(command, user, Guid.Empty);
        }

        public void Dispatch<T>(T command, Guid objectId) where T : ICommand
        {
            Dispatch(command, null, objectId);
        }

        public void Dispatch<T>(T command, IPrincipal user, Guid objectId) where T : ICommand
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
            handler.Handle(command);
        }
    }
}
