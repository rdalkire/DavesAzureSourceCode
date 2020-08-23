using Microsoft.AspNetCore.Http;

/*****
 * The original exercise, found at [Exercise - Unit test an Azure Function](https://docs.microsoft.com/en-us/learn/modules/develop-test-deploy-azure-functions-with-visual-studio/6-unit-test-azure-functions),
 * is not compatible with dotnet core 3.1, because Microsoft stopped supporting
 * "Pupternal" APIs. See the commented-out using statement below.
 * Instead I'm using an article about integration tests at docs.microsoft.com 
 * 
 ****/


// See [..."Pubternal" APIs removed](https://docs.microsoft.com/en-us/dotnet/core/compatibility/2.2-3.1#pubternal-apis-removed)
// using Microsoft.AspNetCore.Http.Internal;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using Xunit;

namespace WatchFunctionTests
{
    public class WatchFunctionUnitTests
    {
        // [Fact]
        public void TestWatchFunctionSuccess()
        {
            //var httpContext = new DefaultHttpContext();
            //var queryStringValue = "abc";
            //var request = new DefaultHttpRequest(new DefaultHttpContext())
            //{
            //    Query = new QueryCollection
            //    (
            //        new System.Collections.Generic.Dictionary<string, StringValues>()
            //        {
            //{ "model", queryStringValue }
            //        }
            //    )
            //};
        }
    }
}
