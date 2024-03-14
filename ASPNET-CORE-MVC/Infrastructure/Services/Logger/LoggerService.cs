using Domain.Services.LoggerInterface;
using Serilog;

namespace Infrastructure.Services.Logger
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger _logger;

        public LoggerService(ILogger logger)
        {
            _logger = logger;
        }
        public void LogInfo(string infoMsg)
        {
            _logger.Information(infoMsg);
        }
        public void LogWarn(string warnMsg)
        {
            _logger.Warning(warnMsg);
        }
        public void LogError(string errorMsg)
        {
            _logger.Error(errorMsg);
        }
    }
}
