using System;

namespace Zuravvski.Infrastructure.Exceptions.Abstractions
{
    public interface IExceptionToResponseMapper
    {
        ExceptionResponse Map(Exception exception);
    }
}
