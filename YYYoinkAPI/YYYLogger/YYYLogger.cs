using Serilog;
using ILogger = Serilog.ILogger;

namespace YYYoinkAPI.Logger;

public class YYYLogger
{
    public ILogger Log = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.File("./logs/log.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();
}