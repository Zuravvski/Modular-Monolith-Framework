namespace Zuravvski.Infrastructure.Cqrs.Abstractions.Commands
{
    public interface ICommand
    {
        // Marker interface
    }

    public interface ICommand<out TResult>
    {
        // Marker interface
    }
}
