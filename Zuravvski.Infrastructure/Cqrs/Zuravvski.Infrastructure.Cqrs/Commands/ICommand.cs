namespace Zuravvski.Infrastructure.Cqrs.Commands
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
