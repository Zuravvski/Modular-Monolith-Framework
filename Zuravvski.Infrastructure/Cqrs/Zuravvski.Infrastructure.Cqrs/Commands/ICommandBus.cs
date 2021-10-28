using System.Threading.Tasks;

namespace Zuravvski.Infrastructure.Cqrs.Commands
{
    public interface ICommandBus
    {
        public Task Dispatch<TCommand>(TCommand command) where TCommand : class, ICommand;
        public Task<TResult> Dispatch<TResult>(ICommand<TResult> command);
    }
}
