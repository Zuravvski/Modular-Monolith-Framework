using System;
using System.Threading.Tasks;
using Autofac;
using Zuravvski.Infrastructure.Cqrs.Abstractions.Commands;

namespace Zuravvski.Infrastructure.Cqrs.Memory.Autofac
{
    internal sealed class InMemoryCommandBus : ICommandBus
    {
        private readonly IComponentContext _context;

        public InMemoryCommandBus(IComponentContext context)
        {
            _context = context;
        }

        public async Task Dispatch<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(TCommand), "Command cannot be null");
            }

            var didResolveHandler = _context.TryResolve(out ICommandHandler<TCommand> handler);

            if (didResolveHandler)
            {
                await handler.Handle(command);
            }
        }

        public async Task<TResult> Dispatch<TResult>(ICommand<TResult> command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(ICommand<TResult>), "Command cannot be null");
            }

            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
            dynamic handler = _context.Resolve(handlerType);
            return await handler.Handle((dynamic)command);
        }
    }
}
