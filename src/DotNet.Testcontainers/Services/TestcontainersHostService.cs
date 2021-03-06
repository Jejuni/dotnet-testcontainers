namespace DotNet.Testcontainers.Services
{
  using System.IO;
  using DotNet.Testcontainers.Internals;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;
  using Microsoft.Extensions.Logging;
  using Serilog;
  using ILogger = Microsoft.Extensions.Logging.ILogger;

  internal static class TestcontainersHostService
  {
    private static readonly IHost Host = InitHost();

    public static ILogger GetLogger(string categoryName)
    {
      return Host.Services.GetRequiredService<ILoggerFactory>().CreateLogger(categoryName);
    }

    public static ILogger<T> GetLogger<T>()
    {
      return Host.Services.GetRequiredService<ILogger<T>>();
    }

    private static IHost InitHost()
    {
      return new HostBuilder()
        .ConfigureAppConfiguration((hostContext, config) =>
        {
          config.SetBasePath(Directory.GetCurrentDirectory());
        })
        .ConfigureLogging((hostContext, config) =>
        {
          config.AddSerilog(TestcontainersLoggerConfiguration.Production.CreateLogger(), true);
        })
        .Build();
    }
  }
}
