# Azure Functions and Testing

I started this because exercise, found at [Exercise - Unit test an Azure Function](https://docs.microsoft.com/en-us/learn/modules/develop-test-deploy-azure-functions-with-visual-studio/6-unit-test-azure-functions),
 is not compatible with dotnet core 3.1, because Microsoft stopped supporting
 "Pupternal" APIs, specifically for this case,
 Microsoft.AspNetCore.Http.Internal.DefaultHttpRequest
 See [..."Pubternal" APIs removed](https://docs.microsoft.com/en-us/dotnet/core/compatibility/2.2-3.1#pubternal-apis-removed)

 I attempted to use guidance from the article [Integration tests in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1), hoping to use utilities found in Microsoft.AspNetCore.Mvc.Testing to test azure functions.  So far this does not bear fruit - See WatchFunction.IntegrationTests - at the time of this edit, the latest test failure is "System.InvalidOperationException : Having multiple overloads of method 'Configure' is not supported."

 However I find that there's another way to avoid the use of that 'Http.Internal namespace.  Consider these lines:

``` c#
using Microsoft.AspNetCore.Http;

// ...
// class and method definitions...
// ...

	var req = new Microsoft.AspNetCore.Http.Internal.
		DefaultHttpRequest( new DefaultHttpContext() );
```

Simply change how you're getting that request like so:
``` c#
using Microsoft.AspNetCore.Http;

// ...
// class and method definitions...
// ...

	var req = new DefaultHttpContext().Request;
```

That's it!  No more dependence on things that were not intended for your use in the first place.

But I still wanted to use dependency injection to support integration testing, so I turned to a blog article [Integration Testing in Azure Functions with Dependency Injection](https://saebamini.com/integration-testing-in-azure-functions-with-dependency-injection/), by Saeb Amini, and his example/template Github project [Saeb.FunctionApp](https://github.com/SaebAmini/Saeb.FunctionApp/blob/master/Saeb.FunctionApp.IntegrationTests/SuperFunctionTests.cs).  You can see the way I adopted his work in my test class 
WatchFunction.TestsFromExercise.WatchFunctionIntegrationTestLikeSaebs and the dependent classes, which are copies from his "Infrastructure" directory.