using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.Serializers;
using MavFiFoundation.SourceGenerators.Testing.Models;

namespace MavFiFoundation.SourceGenerators.UnitTests.TypeLocators;

public class MFFAttributeTypeLocatorTests
{
    private readonly Mock<IMFFSerializer> _mockSerializer;

    public MFFAttributeTypeLocatorTests()
    {
        _mockSerializer = new Mock<IMFFSerializer>(MockBehavior.Strict);
    }

    [Fact]
    public void GetTypesWithAttribute_ReturnsEmptySourceTypes_WhenNoClassesWithMatchingAttributeFound ()
    {
        //Arrange

        var genInfos = new MFFGeneratorInfoRecord?[]
        {
            new MFFGeneratorInfoRecordBuilder()
                .SrcLocatorType(GeneratorConstants.TypeLocator.MFFAttributeTypeLocator)
                .Build()
        }.ToImmutableArray();

        var srcSymbols = new MFFTypeSymbolSources[] 
        {
            new MFFTypeSymbolSourcesBuilder()
                .AddType(new MFFTypeSymbolRecordBuilder()
                    .Build())
                .Build(),
        }.ToImmutableArray();

        var cancellationToken = new CancellationToken();
        var cut = new MFFAttributeTypeLocatorTestClass(_mockSerializer.Object);

        //Act
        var actual = cut.ExposedGetTypesWithAttribute(genInfos, srcSymbols, cancellationToken);

        //Assert
        actual.Should().HaveCount(1);
        var actual_0 = actual[0];
        actual_0.Should().NotBeNull();
        actual_0.GenInfo.Should().BeSameAs(genInfos[0]);
        actual_0.SrcTypes.Should().BeEmpty();
    }
}
