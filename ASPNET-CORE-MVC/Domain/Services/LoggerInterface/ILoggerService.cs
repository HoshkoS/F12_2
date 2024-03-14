namespace Domain.Services.LoggerInterface
{
    public interface ILoggerService
    {
        void LogInfo(string infoMsg);
        void LogWarn(string warnMsg);
        void LogError(string errorMsg);
    }
}
