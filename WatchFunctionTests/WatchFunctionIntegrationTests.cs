/*****
 * The original exercise, found at [Exercise - Unit test an Azure Function](https://docs.microsoft.com/en-us/learn/modules/develop-test-deploy-azure-functions-with-visual-studio/6-unit-test-azure-functions),
 * is not compatible with dotnet core 3.1, because Microsoft stopped supporting
 * "Pupternal" APIs, specifically for this case,
 * Microsoft.AspNetCore.Http.Internal.DefaultHttpRequest
 * See [..."Pubternal" APIs removed](https://docs.microsoft.com/en-us/dotnet/core/compatibility/2.2-3.1#pubternal-apis-removed)
 *
 * Instead I'm using guidance from the article [Integration tests in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1)
 * 
 ****/

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using WatchFunction.FunctionApp;
using WatchFunction.IntegrationTests.Infrastructure;
using Xunit;

namespace WatchFunction.IntegrationTests
{
    public class WatchFunctionIntegrationTests : 
        IClassFixture<TestWebApplicationFactory<TestStartup>>
    {
        private readonly TestWebApplicationFactory<TestStartup> _factory;

        public WatchFunctionIntegrationTests(
            TestWebApplicationFactory<TestStartup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task TestWatchFunctionSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();

            // var request = client

            // Act
            var response = await client.GetAsync("/?model=any");

            // Assert
            Assert.True( response.IsSuccessStatusCode );
        }
    }
}
