using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleCpuMonitor.Configuration;
using SimpleCpuMonitor.Container;
using SimpleCpuMonitor.Worker;
using StructureMap;

namespace SimpleCpuMonitor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _container = CompositionRoot.GetContainer(Configuration);

            var cpuConfigs = _container.GetInstance<CpuMonitoringConfiguration>();
            CpuMonitoring.CPUOverloadEvent += Sender.SendOverloadInfo;
            CpuMonitoring.StartCpuMonitoring(cpuConfigs.MaxLoad, cpuConfigs.Interval);
        }

        public IConfiguration Configuration { get; }
        private IContainer _container;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            _container.Populate(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
