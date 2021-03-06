using Serilog;

namespace Zuravvski.Infrastructure.Logging.Serilog
{
    public static class Extensions
    {
        private static readonly string ModuleTag = "Module";

        public static ILogger ForModule(this ILogger logger, string moduleName)
            => logger.ForContext(ModuleTag, moduleName);
    }
}
