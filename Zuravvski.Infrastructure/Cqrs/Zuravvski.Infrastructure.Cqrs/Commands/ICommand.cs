﻿namespace Zuravvski.Infrastructure.Cqrs.Commands.Abstractions
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