using Microsoft.Extensions.DependencyInjection;
using Zuravvski.Infrastructure.Cqrs.Abstractions.Commands;

namespace Zuravvski.Infrastructure.Cqrs.Memory.DependencyInjection
{
    internal sealed class InMemoryCommandBus : ICommandBus
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public InMemoryCommandBus(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Dispatch<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(TCommand), "Command cannot be null");
            }

            using var scope = _serviceScopeFactory.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            await handler?.Handle(command);
        }

        public async Task<TResult> Dispatch<TResult>(ICommand<TResult> command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(ICommand<TResult>), "Command cannot be null");
            }

            using var scope = _serviceScopeFactory.CreateScope();
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
            var handler = scope.ServiceProvider.GetRequiredService(handlerType);
            return await(Task<TResult>) handlerType
               .GetMethod(nameof(ICommandHandler<ICommand<TResult>, TResult>.Handle))
               ?.Invoke(handler, new[] { command });
        }
    }
}
