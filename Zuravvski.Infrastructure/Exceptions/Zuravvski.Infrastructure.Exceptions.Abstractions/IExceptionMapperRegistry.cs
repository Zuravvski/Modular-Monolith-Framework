using System;

namespace Zuravvski.Infrastructure.Exceptions.Abstractions
{
    public interface IExceptionMapperRegistry
    {
        public void Register<TExceptionMapper>(string @namespace = null)
            where TExceptionMapper : class, IExceptionToResponseMapper, new();

        public void RegisterFallbackMapper<TFallbackMapper>() where TFallbackMapper : class, IExceptionToResponseMapper, new();
        public IExceptionToResponseMapper Resolve(Exception ex);
    }
}
