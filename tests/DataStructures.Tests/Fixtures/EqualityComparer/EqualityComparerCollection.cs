using Xunit;

namespace DataStructures.Tests.Fixtures.EqualityComparer
{
    [CollectionDefinition(nameof(EqualityComparerCollection))]
    public class EqualityComparerCollection : ICollectionFixture<EqualityComparerFixture>
    { }
}
