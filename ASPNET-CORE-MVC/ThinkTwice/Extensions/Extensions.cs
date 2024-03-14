using Domain.Services.LoggerInterface;
using Infrastructure.Services.Logger;

namespace ThinkTwice.Extensions
{
    public static class Extensions
    {
        public static void AddSerilog(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerService, LoggerService>();
        }

    }
}
