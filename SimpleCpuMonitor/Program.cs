using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SimpleCpuMonitor.Worker;

namespace SimpleCpuMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {

            CpuMonitoring.StartCpuMonitoring();
            CpuMonitoring.CPUOverloadEvent += Sender.SendOverloadInfo;
            CreateWebHostBuilder(args).Build().Run();
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
