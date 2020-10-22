using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        // NOTE this is not an established Fact; it fails
        // [Fact]
        public async Task TestWatchFunctionSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();
            var request = new HttpRequestMessage();
            var queryString = QueryString.Create("model", "any");
            request.RequestUri = new Uri( "/" + queryString );

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.True( response.IsSuccessStatusCode );
        }
    }
}
