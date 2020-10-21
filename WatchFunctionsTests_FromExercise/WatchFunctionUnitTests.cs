using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using WatchFunction.Domain;
using WatchFunction.FunctionApp;
using Xunit;

namespace WatchFunction.TestsFromExercise
{
    class TestWatchProvider : IWatchInfoProvider
    {
        public WatchItem Item { get; set; }

        public WatchItem ProvideWatchItem( string model )
        {
            return Item;
        }
    }
    public class WatchFunctionUnitTests
    {
        static readonly IWatchInfoProvider TestProvider= new TestWatchProvider()
        {
            Item = new WatchItem()
            {
                Manufacturer = "Abc",
                CaseType = "Solid",
                Bezel = "Titanium",
                Dial = "Roman",
                CaseFinish = "Silver",
                Jewels = 15
            }
        };

        [Fact]
        public void TestWatchFunctionSuccess()
        {
            var request = new DefaultHttpContext().Request;
            request.QueryString = QueryString.Create("model", "any");

            var logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");

            var response = new WatchInfoFunction(TestProvider).
                Run(request, logger);

            // Check that the response is an "OK" response
            Assert.IsAssignableFrom<OkObjectResult>(response);

            // Check that the contents of the response are the expected contents
            var result = (OkObjectResult)response;

            var watchinfo = TestProvider.ProvideWatchItem("realModel");

            string watchInfo = 
                $"Watch Details: {watchinfo.Manufacturer}, " +
                $"{watchinfo.CaseType}, {watchinfo.Bezel}, {watchinfo.Dial}, " +
                $"{watchinfo.CaseFinish}, {watchinfo.Jewels}";

            Assert.Equal(watchInfo, result.Value);
        }

        [Fact]
        public void TestWatchFunctionFailureNoQueryString()
        {
            var request = new DefaultHttpContext().Request;
            var logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");

            var response = new WatchInfoFunction(TestProvider).
                Run(request, logger);

            // Check that the response is an "Bad" response
            Assert.IsAssignableFrom<BadRequestObjectResult>(response);

            // Check that the contents of the response are the expected contents
            var result = (BadRequestObjectResult)response;
            Assert.Equal("Please provide a watch model in the query string", 
                result.Value);
        }

        [Fact]
        public void TestWatchFunctionFailureNoModel()
        {
            var request = new DefaultHttpContext().Request;
            request.QueryString = QueryString.Create("not-model", "any");

            var logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");

            var response = new WatchInfoFunction(TestProvider).
                Run(request, logger);

            // Check that the response is an "Bad" response
            Assert.IsAssignableFrom<BadRequestObjectResult>(response);

            // Check that the contents of the response are the expected contents
            var result = (BadRequestObjectResult)response;

            Assert.Equal("Please provide a watch model in the query string", 
                result.Value);
        }
    }
}
