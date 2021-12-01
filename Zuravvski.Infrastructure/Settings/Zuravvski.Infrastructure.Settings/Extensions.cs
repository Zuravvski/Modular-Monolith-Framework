using System;
using Microsoft.Extensions.Configuration;

namespace Zuravvski.Infrastructure.Settings
{
    public static class Extensions
    {
        public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string optionsSectionName = null, bool optional = false)
            where TOptions : class, new()
        {
            var sectionName = optionsSectionName ?? typeof(TOptions).Name.Replace("Options", string.Empty);
            var section = configuration.GetSection(sectionName);

            if (!section.Exists())
            {
                if (!optional)
                {
                    throw new InvalidOperationException($"A section called {sectionName} is missing in appsettings.json");
                }
                else
                {
                    return null;
                }
            }

            var options = new TOptions();
            section.Bind(options);
            return options;
        }
    }
}
