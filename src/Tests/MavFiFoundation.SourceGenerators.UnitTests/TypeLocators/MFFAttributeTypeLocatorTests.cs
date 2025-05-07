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
                .Build()
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

    [Fact]
    public void GetTypesWithAttribute_ReturnsEmptyArray_WhenGenInfoIsNull ()
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
        var cut = new MFFAttributeTypeLocatorTestClass(_mockSerializer.Object);

        //Act
        var actual = cut.ExposedGetTypesWithAttribute(genInfos, srcSymbols, cancellationToken);

        //Assert
        actual.Should().BeEmpty();
    }

    [Fact]
    public void GetTypesWithAttribute_WhenAttributeNameProvidedAsString_ReturnsTypesWithMatchingAttribute ()
    {
        //Arrange

        var attribute2Find = "TestSpace.TestAttribute";

        var genInfos = new MFFGeneratorInfoRecord?[]
        {
            new MFFGeneratorInfoRecordBuilder()
                .SrcLocatorType(GeneratorConstants.TypeLocator.MFFAttributeTypeLocator)
                .SrcLocatorInfo(attribute2Find)
                .Build()
        }.ToImmutableArray();

        var matchingType = new MFFTypeSymbolRecordBuilder()
                    .AddAttribute(new MFFAttributeDataRecordBuilder()
                        .Name(attribute2Find)
                        .Build())
                    .Build();

        var srcSymbols = new MFFTypeSymbolSources[] 
        {
            new MFFTypeSymbolSourcesBuilder()
                .AddType(new MFFTypeSymbolRecordBuilder()
                    .Build())
                .Build(),
            new MFFTypeSymbolSourcesBuilder()
                .AddType(matchingType)
                .Build()
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
        actual_0.SrcTypes.Should().HaveCount(1);
        var srcTypes_0 = actual_0.SrcTypes[0];
        srcTypes_0.Should().BeSameAs(matchingType);
    }

    [Fact]
    public void GetTypesWithAttribute_WhenAttributeNameProvidedSerialized_ReturnsTypesWithMatchingAttribute ()
    {
        //Arrange

        var attribute2Find = "TestSpace.TestAttribute";
        var serializedLocatorInfo = 
$$"""
{
    attribute2Find: "{{attribute2Find}}"
}
""";

        _mockSerializer.Setup(x => x.DeserializeObject<MFFAttributeTypeLocatorInfo>(It.Is<string>(s => s == serializedLocatorInfo)))
            .Returns(new MFFAttributeTypeLocatorInfo()
                {
                    Attribute2Find = attribute2Find
                })
            .Verifiable(Times.Once);

        var genInfos = new MFFGeneratorInfoRecord?[]
        {
            new MFFGeneratorInfoRecordBuilder()
                .SrcLocatorType(GeneratorConstants.TypeLocator.MFFAttributeTypeLocator)
                .SrcLocatorInfo(serializedLocatorInfo)
                .Build()
        }.ToImmutableArray();

        var matchingType = new MFFTypeSymbolRecordBuilder()
                    .AddAttribute(new MFFAttributeDataRecordBuilder()
                        .Name(attribute2Find)
                        .Build())
                    .Build();

        var srcSymbols = new MFFTypeSymbolSources[] 
        {
            new MFFTypeSymbolSourcesBuilder()
                .AddType(new MFFTypeSymbolRecordBuilder()
                    .Build())
                .Build(),
            new MFFTypeSymbolSourcesBuilder()
                .AddType(matchingType)
                .Build()
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
        actual_0.SrcTypes.Should().HaveCount(1);
        var srcTypes_0 = actual_0.SrcTypes[0];
        srcTypes_0.Should().BeSameAs(matchingType);
        _mockSerializer.VerifyAll();
    }

    [Fact]
    public void GetTypesWithAttribute_WhenExternalAssemblyIsSpecified_ReturnsTypesWithMatchingAttribute ()
    {
        //Arrange

        var attribute2Find = "TestSpace.TestAttribute";
        var assembly2Search = "SomeExternalAssembly";

        var serializedLocatorInfo = 
$$"""
{
    attribute2Find: "{{attribute2Find}}",
    assemblies2Search: "{{assembly2Search}}
}
""";

        _mockSerializer.Setup(x => x.DeserializeObject<MFFAttributeTypeLocatorInfo>(It.Is<string>(s => s == serializedLocatorInfo)))
            .Returns(new MFFAttributeTypeLocatorInfo()
                {
                    Attribute2Find = attribute2Find,
                    Assemblies2Search = new string[]{ assembly2Search }
                })
            .Verifiable(Times.Once);

        var genInfos = new MFFGeneratorInfoRecord?[]
        {
            new MFFGeneratorInfoRecordBuilder()
                .SrcLocatorType(GeneratorConstants.TypeLocator.MFFAttributeTypeLocator)
                .SrcLocatorInfo(serializedLocatorInfo)
                .Build()
        }.ToImmutableArray();

        var matchingType = new MFFTypeSymbolRecordBuilder()
                    .AddAttribute(new MFFAttributeDataRecordBuilder()
                        .Name(attribute2Find)
                        .Build())
                    .Build();

        var srcSymbols = new MFFTypeSymbolSources[] 
        {
            new MFFTypeSymbolSourcesBuilder()
                .Source(assembly2Search)
                .AddType(new MFFTypeSymbolRecordBuilder()
                    .Build())
                .Build(),
            new MFFTypeSymbolSourcesBuilder()
                .AddType(matchingType)
                .Build()
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
        actual_0.SrcTypes.Should().HaveCount(1);
        var srcTypes_0 = actual_0.SrcTypes[0];
        srcTypes_0.Should().BeSameAs(matchingType);
        _mockSerializer.VerifyAll();
    }

    [Fact]
    public void GetTypesWithAttribute_WhenTypes2ExcludeProvidedSerialized_DoesNotReturnExcludedType ()
    {
        //Arrange

        var attribute2Find = "TestSpace.TestAttribute";
        var type2Exclude = "TestSpace.Type2Exclude";
        var serializedLocatorInfo = 
$$"""
{
    attribute2Find: "{{attribute2Find}}",
    Types2Exclude: ["{{type2Exclude}}"]
}
""";

        _mockSerializer.Setup(x => x.DeserializeObject<MFFAttributeTypeLocatorInfo>(It.Is<string>(s => s == serializedLocatorInfo)))
            .Returns(new MFFAttributeTypeLocatorInfo()
                {
                    Attribute2Find = attribute2Find,
                    Types2Exclude = new string[]{ type2Exclude }
                })
            .Verifiable(Times.Once);

        var genInfos = new MFFGeneratorInfoRecord?[]
        {
            new MFFGeneratorInfoRecordBuilder()
                .SrcLocatorType(GeneratorConstants.TypeLocator.MFFAttributeTypeLocator)
                .SrcLocatorInfo(serializedLocatorInfo)
                .Build()
        }.ToImmutableArray();

        var matchingType = new MFFTypeSymbolRecordBuilder()
            .AddAttribute(new MFFAttributeDataRecordBuilder()
                .Name(attribute2Find)
                .Build())
            .Build();

        var excludedType = new MFFTypeSymbolRecordBuilder()
            .FullyQualifiedName(type2Exclude)
            .AddAttribute(new MFFAttributeDataRecordBuilder()
                .Name(attribute2Find)
                .Build())
            .Build();

        var srcSymbols = new MFFTypeSymbolSources[] 
        {
            new MFFTypeSymbolSourcesBuilder()
                .AddType(excludedType)
                .Build(),
            new MFFTypeSymbolSourcesBuilder()
                .AddType(matchingType)
                .Build()
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
        actual_0.SrcTypes.Should().NotContain(excludedType);
        actual_0.SrcTypes.Should().HaveCount(1);
        var srcTypes_0 = actual_0.SrcTypes[0];
        srcTypes_0.Should().BeSameAs(matchingType);
        _mockSerializer.VerifyAll();
    }

}
