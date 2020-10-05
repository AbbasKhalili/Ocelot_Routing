using NLog;

namespace ApiGateway
{
    public class NloggerService : ILoggerService
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        public void Information(string message)
        {
            Logger.Info(message);
        }
    }
}