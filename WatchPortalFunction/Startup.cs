using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WatchFunction.Domain;
using WatchFunction.FunctionApp;

[assembly: FunctionsStartup(typeof(Startup))]

namespace WatchFunction.FunctionApp
{
    class Startup: FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IWatchInfoProvider, WatchInfoProvider>();
        }
    }
}
