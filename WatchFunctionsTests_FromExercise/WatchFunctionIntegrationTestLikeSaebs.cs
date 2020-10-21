using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using WatchFunction.Domain;
using WatchFunction.FunctionApp;
using WatchFunction.TestsFromExercise.Infrastructure;
using Xunit;
using Xunit.Abstractions;

namespace WatchFunction.TestsFromExercise
{
    [Collection(TestsCollection.Name)]
    public class WatchFunctionIntegrationTestLikeSaebs
    { 
        readonly WatchInfoFunction _sut;
        private readonly ITestOutputHelper _outputHelper;

        public WatchFunctionIntegrationTestLikeSaebs(TestHost testHost, 
            ITestOutputHelper outputHelp )
        {
            _sut = new WatchInfoFunction( testHost.ServiceProvider.
                    GetRequiredService<IWatchInfoProvider>() );

            _outputHelper = outputHelp;
        }

        [Fact]
        public void Test()
        {
            // arrange
            var context = new DefaultHttpContext();

            var req = new Microsoft.AspNetCore.Http.Internal.
                DefaultHttpRequest(context);

            req.QueryString = QueryString.Create("model", "any");
            var logger = NullLoggerFactory.Instance.CreateLogger("Null Logger");

            // act
            var result = (ObjectResult) _sut.Run(req, logger);
            var resultValue = result.Value.ToString()?? "";
            _outputHelper.WriteLine( "resultValue: "+ resultValue );

            // assert
            Assert.Equal( 200, result.StatusCode );

            Assert.StartsWith( 
                "Watch Details: We Don't Make Watches", resultValue );
        }

    }
}
