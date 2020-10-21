﻿using Xunit;

namespace WatchFunction.IntegrationTests.Infrastructure
{
	[CollectionDefinition(Name)]
	public class TestsCollection : ICollectionFixture<TestHost>
	{
		public const string Name = nameof(TestsCollection);
	}
}
