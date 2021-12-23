using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.RegularExpressions;
using Zuravvski.Infrastructure.Exceptions.Abstractions;

namespace Zuravvski.Infrastructure.Exceptions.Middleware
{
    internal sealed class DefaultExceptionMapperRegistry : IExceptionMapperRegistry
    {
        private readonly ConcurrentDictionary<Regex, IExceptionToResponseMapper> _exceptionMapperRegistry = new();
        private IExceptionToResponseMapper _fallbackMapper;

        public void Register<TExceptionMapper>(string namespaceRegex = null)
            where TExceptionMapper : class, IExceptionToResponseMapper, new()
        {
            namespaceRegex ??= typeof(TExceptionMapper).Namespace;

            if (namespaceRegex is null)
            {
                throw new ArgumentNullException(nameof(namespaceRegex), "Cannot register an exception mapper due to invalid namespace");
            }

            _exceptionMapperRegistry.TryAdd(new Regex(namespaceRegex), new TExceptionMapper());
        }

        public void RegisterFallbackMapper<TFallbackMapper>() where TFallbackMapper : class, IExceptionToResponseMapper, new()
        {
            if (_fallbackMapper is { })
            {
                throw new InvalidOperationException("Fallback exception mapper has already been registered");
            }
            _fallbackMapper = new TFallbackMapper();
        }

        public IExceptionToResponseMapper Resolve(Exception ex)
        {
            var matchingEntry = _exceptionMapperRegistry.SingleOrDefault(mapperEntry => mapperEntry.Key.IsMatch(ex.Source));
            return matchingEntry.IsDefault() ? _fallbackMapper : matchingEntry.Value;
        }
    }
}
