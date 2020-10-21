using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WatchFunction.FunctionApp;

namespace WatchFunction.IntegrationTests.Infrastructure
{
	public class TestHost
	{
        public IHostBuilder HostBuilder { get; set; }

		public TestHost()
		{
			var startup = new TestStartup();

            HostBuilder = new HostBuilder()
                .ConfigureWebJobs(startup.Configure)
                .ConfigureServices(ReplaceTestOverrides);

            // TODO clean up commented code
			// var host = HostBuilder.Build();

			// ServiceProvider = host.Services;
		}

        // public IServiceProvider ServiceProvider { get; }

		private void ReplaceTestOverrides(IServiceCollection services)
		{
			// services.Replace(new ServiceDescriptor(typeof(ServiceToReplace), testImplementation));
		}

	}
    public class TestStartup : Startup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            SetExecutionContextOptions(builder);
            base.Configure(builder);
        }

        private static void SetExecutionContextOptions(IFunctionsHostBuilder builder)
        {
            builder.Services.Configure<ExecutionContextOptions>(o => o.AppDirectory = Directory.GetCurrentDirectory());
        }

    }

    public class TestWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        //protected override IHostBuilder CreateHostBuilder()
        //{
        //    var builder = new TestHost().HostBuilder;
        //    return builder;
        //}

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            // var startup = new TestStartup();

            var builder = new WebHostBuilder().
                UseStartup(typeof(TestStartup)).
                ConfigureServices(ReplaceTestOverrides);

            return builder;
        }
        private void ReplaceTestOverrides(IServiceCollection services)
        {
            // services.Replace(new ServiceDescriptor(typeof(ServiceToReplace), testImplementation));
        }
    }

}
