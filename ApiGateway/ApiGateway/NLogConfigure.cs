using NLog;

namespace ApiGateway
{
    public static class NLogConfigure
    {
        public static void Config(string nlogConfigFile = "nlog.config")
        {
            LogManager.LoadConfiguration(nlogConfigFile).GetCurrentClassLogger();
        }
    }
}