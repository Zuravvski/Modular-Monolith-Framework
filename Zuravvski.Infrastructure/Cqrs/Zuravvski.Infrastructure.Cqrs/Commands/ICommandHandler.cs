using System.Threading.Tasks;

namespace Zuravvski.Infrastructure.Cqrs.Commands.Abstractions
{
    public interface ICommandHandler<TCommand> where TCommand : class, ICommand
    {
        public Task Handle(TCommand command);
    }

    public interface ICommandHandler<in TCommand, TResult> where TCommand : class, ICommand<TResult>
    {
        public Task<TResult> Handle(TCommand command);
    }
}
