using Microsoft.Extensions.Configuration;
using Serilog;
using SimpleCpuMonitor.Configuration;
using StructureMap;
using System.IO;

namespace SimpleCpuMonitor.Container
{
    public static class CompositionRoot
    {
        public static IContainer Container { get; private set; }

        public static IContainer GetContainer(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               .MinimumLevel.Debug()
               .WriteTo.File(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "logs", "log.log"),
               rollingInterval: RollingInterval.Day)
               .CreateLogger();

            Log.Logger.Information("Configuring container...");
            Container = new StructureMap.Container();

            Container.Configure(config =>
            {
                //Конфигурации RabbitMQ
                config.ForSingletonOf<RabbitMQConfigurations>().Use(new RabbitMQConfigurations
                {
                    Exchange = configuration["RabbitMQ:Exchange"],
                    Host = configuration["RabbitMQ:Host"],
                    Password = configuration["RabbitMQ:Password"],
                    RoutingKey = configuration["RabbitMQ:RoutingKey"],
                    Type = configuration["RabbitMQ:Type"],
                    User = configuration["RabbitMQ:User"],
                });

                //Конфигурации мониторинга
                config.ForSingletonOf<CpuMonitoringConfiguration>().Use(new CpuMonitoringConfiguration
                {
                    MaxLoad = float.Parse(configuration["Monitoring:MaxLoad"]),
                    Interval = int.Parse(configuration["Monitoring:Interval"])
                });

                //Конфигурации снимка
                config.ForSingletonOf<SnapshotConfiguration>().Use(new SnapshotConfiguration
                {
                    Font = configuration["Snapshot:Font"],
                    FontSize = int.Parse(configuration["Snapshot:FontSize"])
                });
            });

            return Container;
        }
    }
}
