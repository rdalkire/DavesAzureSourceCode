using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using WatchFunction.Domain;

namespace WatchFunction.FunctionApp
{
    public class WatchInfoFunction
    {
        readonly IWatchInfoProvider _watchInfoProvider;

        public WatchInfoFunction(IWatchInfoProvider watchInfoProvider)
        {
            _watchInfoProvider = watchInfoProvider;
        }

        [FunctionName("WatchInfo")]
        public IActionResult Run( [HttpTrigger(AuthorizationLevel.Anonymous, 
                "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Retrieve the model id from the query string
            string model = req.Query["model"];

            // If the user specified a model id, find the details of the model
            // of watch
            if (model != null)
            {
                WatchItem watchInfo = 
                    _watchInfoProvider.ProvideWatchItem( model );

                return new OkObjectResult(
                    $"Watch Details: {watchInfo.Manufacturer}, " +
                    $"{watchInfo.CaseType}, {watchInfo.Bezel}, " +
                    $"{watchInfo.Dial}, {watchInfo.CaseFinish}, " +
                    $"{watchInfo.Jewels}");
            }
            return new BadRequestObjectResult(
                "Please provide a watch model in the query string");
        }
    }
}
