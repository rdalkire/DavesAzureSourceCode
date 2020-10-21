using Xunit;

namespace WatchFunction.TestsFromExercise.Infrastructure
{
	[CollectionDefinition(Name)]
	public class TestsCollection : ICollectionFixture<TestHost>
	{
		public const string Name = nameof(TestsCollection);
	}
}
