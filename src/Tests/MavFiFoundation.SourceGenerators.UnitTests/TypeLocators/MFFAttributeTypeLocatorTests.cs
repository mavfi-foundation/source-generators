using System.Collections.Immutable;

using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.Serializers;

namespace MavFiFoundation.SourceGenerators.UnitTests.TypeLocators;

public class MFFAttributeTypeLocatorTests
{
    private readonly Mock<IMFFSerializer> _mockSerializer;

    public MFFAttributeTypeLocatorTests()
    {
        _mockSerializer = new Mock<IMFFSerializer>(MockBehavior.Strict);
    }

    [Fact]
    public void GetTypesWithAttribute_ReturnsEmptySourceTypes_WhenNoClassesWithMatchingAttribteFound ()
    {
        //Arrange

        var genInfos = new MFFGeneratorInfoRecord?[]
        {
            new MFFGeneratorInfoRecord
            (
                "TestSpace",
                "MFFAttributeTypeLocator",
                "SrcLocatorInfo",
                Array.Empty<MFFBuilderRecord>().ToImmutableArray(),
                Array.Empty<MFFBuilderRecord>().ToImmutableArray()
            )
        }.ToImmutableArray();

        var srcSymbols = new MFFTypeSymbolSources[] 
        {
            new MFFTypeSymbolSources
            (
                MFFGeneratorConstants.Generator.COMPILING_PROJECT,
                new MFFTypeSymbolRecord[]
                {
                    new MFFTypeSymbolRecord
                    (
                        "TestSpace",
                        "Name",
                        "GenericParameters",
                        "FullyQualifiedName",
                        "Contraints",
                        false,
                        new MFFPropertySymbolRecord[]{}.ToImmutableArray(),
                        new MFFAttributeDataRecord[]{}.ToImmutableArray()
                    )

                }.ToImmutableArray()
            )
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
