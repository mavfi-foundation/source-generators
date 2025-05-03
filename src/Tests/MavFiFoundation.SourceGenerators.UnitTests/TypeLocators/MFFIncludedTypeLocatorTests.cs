using System;
using System.Collections.Immutable;

using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.UnitTests.TypeLocators;

public class MFFIncludedTypeLocatorTests
{
    [Fact]
    public void GetIncludedType_ReturnsIncludedType ()
    {
        //Arrange

        var includedType = 
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
            );

        var genInfo = new MFFGeneratorInfoRecord(
            "TestSpace",
            "MFFIncludedTypeLocator",
            includedType,
            Array.Empty<MFFBuilderRecord>().ToImmutableArray(),
            Array.Empty<MFFBuilderRecord>().ToImmutableArray()
        );

        var cut = new MFFIncludedTypeLocatorTestClass();

        //Act
        var actual = cut.ExposedGetIncludedType(genInfo);

        //Assert
        actual.Should().NotBeNull();
        actual.GenInfo.Should().BeSameAs(genInfo);
        actual.SrcTypes.Should().Contain(includedType);
    }

    [Fact]
    public void GetIncludedType_ReturnsNull_WhenScrLocatorInfoIsNotMFFTypeSymbolRecord ()
    {
        //Arrange

        var genInfo = new MFFGeneratorInfoRecord(
            "TestSpace",
            "MFFIncludedTypeLocator",
            "SrcLocatorInfo",
            Array.Empty<MFFBuilderRecord>().ToImmutableArray(),
            Array.Empty<MFFBuilderRecord>().ToImmutableArray()
        );

        var cut = new MFFIncludedTypeLocatorTestClass();

        //Act
        var actual = cut.ExposedGetIncludedType(genInfo);

        //Assert
        actual.Should().BeNull();
    }
}
