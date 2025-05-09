using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.Serializers;
using MavFiFoundation.SourceGenerators.Testing.Models;

namespace MavFiFoundation.SourceGenerators.UnitTests.TypeLocators;

public abstract class MFFLinqTypeLocatorBaseTestsBase<T>
    where T: IMFFLinqTypeLocatorBaseTestClass
{

    protected readonly Mock<IMFFSerializer> _mockSerializer;

    public MFFLinqTypeLocatorBaseTestsBase()
    {
        _mockSerializer = new Mock<IMFFSerializer>(MockBehavior.Strict);
    }

    protected abstract T CreateTestClass();

    [Fact]
    public void GetMatchedTypes_ReturnsEmptyArray_WhenGenInfoIsNull ()
    {
        //Arrange

        var genInfos = new MFFGeneratorInfoRecord?[]
        {
            null
        }.ToImmutableArray();

        var srcSymbols = new MFFTypeSymbolSources[] 
        {
            new MFFTypeSymbolSourcesBuilder()
                .AddType(new MFFTypeSymbolRecordBuilder()
                    .Build())
                .Build(),
        }.ToImmutableArray();

        var cancellationToken = new CancellationToken();
        var cut = CreateTestClass();

        //Act
        var actual = cut.ExposedGetMatchedTypes(genInfos, srcSymbols, cancellationToken);

        //Assert
        actual.Should().BeEmpty();
    }

}
