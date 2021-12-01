using System;

namespace Zuravvski.Infrastructure.Exceptions.Abstractions
{
    public abstract class AppException : Exception
    {
        public abstract string Code { get; }

        protected AppException(string message) : base(message)
        {
        }
    }
}
