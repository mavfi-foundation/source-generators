using MavFiFoundation.SourceGenerators.Testing.Models;

namespace MavFiFoundation.SourceGenerators.UnitTests.TypeLocators;

public class MFFIncludedTypeLocatorTests
{
    [Fact]
    public void GetIncludedType_ReturnsIncludedType ()
    {
        //Arrange

        var includedType = 
            new MFFTypeSymbolRecordBuilder()                
                .Build();

        var genInfo = new MFFGeneratorInfoRecordBuilder()
            .SrcLocatorType(GeneratorConstants.TypeLocator.MFFIncludedTypeLocator)
            .SrcLocatorInfo(includedType)
            .Build();

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

        var genInfo = new MFFGeneratorInfoRecordBuilder()
            .SrcLocatorType(GeneratorConstants.TypeLocator.MFFIncludedTypeLocator)
            .Build();

        var cut = new MFFIncludedTypeLocatorTestClass();

        //Act
        var actual = cut.ExposedGetIncludedType(genInfo);

        //Assert
        actual.Should().BeNull();
    }
}
