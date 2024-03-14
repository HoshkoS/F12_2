using Domain.Services.LoggerInterface;
using Serilog;

namespace UseCase.Services.Logger
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger _logger;

        public LoggerService(ILogger logger)
        {
            _logger = logger;
        }
        public void LogInfo(string msg)
        {
            _logger.Information($"{msg}");
        }
        public void LogWarn(string msg)
        {
            _logger.Warning($"{msg}");
        }
        public void LogError(object request, string errorMsg)
        {
            if (request == null)
            {
                _logger.Error(erroMsg);
            }
            else
            {
                string requestType = request.GetType().ToString();
                string requestClass = requestType.Substring(requestType.LastIndexOf('.') + 1);
                _logger.Error($"{requestClass} handled with the error: {erroMsg}");
            }
        }
    }
}
