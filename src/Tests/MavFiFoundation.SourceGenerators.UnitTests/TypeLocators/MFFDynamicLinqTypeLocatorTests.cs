using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.Testing.Models;
using System.Linq.Dynamic.Core;

namespace MavFiFoundation.SourceGenerators.UnitTests.TypeLocators;

public class MFFDynamicLinqTypeLocatorTests
    : MFFLinqTypeLocatorBaseTestsBase<MFFDynamicLinqTypeLocatorTestClass>
{

    protected override MFFDynamicLinqTypeLocatorTestClass CreateTestClass()
    {
        return new MFFDynamicLinqTypeLocatorTestClass(_mockSerializer.Object);
    }

    [Fact]
    public void GetMatchedTypes_ReturnsEmptySourceTypes_WhenNoClassesMatchingNameFound ()
    {
        //Arrange

        var genInfos = new MFFGeneratorInfoRecord?[]
        {
            new MFFGeneratorInfoRecordBuilder()
                .SrcLocatorType(GeneratorConstants.TypeLocator.MFFDynamicLinqTypeLocator)
                .SrcLocatorInfo("Name == \"Foo\"")
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
        var cut = new MFFDynamicLinqTypeLocatorTestClass(_mockSerializer.Object);

        //Act
        var actual = cut.ExposedGetMatchedTypes(genInfos, srcSymbols, cancellationToken);

        //Assert
        actual.Should().HaveCount(1);
        var actual_0 = actual[0];
        actual_0.Should().NotBeNull();
        actual_0.GenInfo.Should().BeSameAs(genInfos[0]);
        actual_0.SrcTypes.Should().BeEmpty();
    }

    [Fact]
    public void GetMatchedTypes_WhenLinqWhereProvidedAsString_ReturnsTypesWithMatchingNamespace ()
    {
        //Arrange
        var containingNamespace = "Test1Space";

        var genInfos = new MFFGeneratorInfoRecord?[]
        {
            new MFFGeneratorInfoRecordBuilder()
                .SrcLocatorType(GeneratorConstants.TypeLocator.MFFDynamicLinqTypeLocator)
                .SrcLocatorInfo($"ContainingNamespace == \"{ containingNamespace }\"")
                .Build()
        }.ToImmutableArray();

        var matchingType = new MFFTypeSymbolRecordBuilder()
                    .ContainingNamespace(containingNamespace)
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
        var t = srcSymbols[0].Types.AsQueryable().Where($"FullyQualifiedName.StartsWith(\"{ MFFTypeSymbolRecordBuilder.DefaultContainingNamespace }.\")");
        var cut = new MFFDynamicLinqTypeLocatorTestClass(_mockSerializer.Object);

        //Act
        var actual = cut.ExposedGetMatchedTypes(genInfos, srcSymbols, cancellationToken);

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
    public void GetMatchedTypes_WhenLinqWhereProvidedSerialized_ReturnsTypesWithMatchingNameSpace ()
    {
        //Arrange

        var containingNamespace = "Test1Space";
        var linqWhere = $"ContainingNamespace == \"{ containingNamespace }\"";
        var serializedLocatorInfo = 
$$"""
{
    linqWhere: "{{ linqWhere }}"
}
""";

        _mockSerializer.Setup(x => x.DeserializeObject<MFFDynamicLinqTypeLocatorInfo>(It.Is<string>(s => s == serializedLocatorInfo)))
            .Returns(new MFFDynamicLinqTypeLocatorInfo()
                {
                    LinqWhere = linqWhere
                })
            .Verifiable(Times.Once);

        var genInfos = new MFFGeneratorInfoRecord?[]
        {
            new MFFGeneratorInfoRecordBuilder()
                .SrcLocatorType(GeneratorConstants.TypeLocator.MFFDynamicLinqTypeLocator)
                .SrcLocatorInfo(serializedLocatorInfo)
                .Build()
        }.ToImmutableArray();

        var matchingType = new MFFTypeSymbolRecordBuilder()
                .ContainingNamespace(containingNamespace)
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
        var cut = new MFFDynamicLinqTypeLocatorTestClass(_mockSerializer.Object);

        //Act
        var actual = cut.ExposedGetMatchedTypes(genInfos, srcSymbols, cancellationToken);

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
    public void GetMatchedTypes_WhenExternalAssemblyIsSpecified_ReturnsTypesWithMatchingNamespace ()
    {
        //Arrange

        var containingNamespace = "Test1Space";
        var linqWhere = $"ContainingNamespace == \"{ containingNamespace }\"";
        var assembly2Search = "SomeExternalAssembly";

        var serializedLocatorInfo = 
$$"""
{
    linqWhere: "{{linqWhere}}",
    assemblies2Search: "{{assembly2Search}}
}
""";

        _mockSerializer.Setup(x => x.DeserializeObject<MFFDynamicLinqTypeLocatorInfo>(It.Is<string>(s => s == serializedLocatorInfo)))
            .Returns(new MFFDynamicLinqTypeLocatorInfo()
                {
                    LinqWhere = linqWhere,
                    Assemblies2Search = new string[]{ assembly2Search }
                })
            .Verifiable(Times.Once);

        var genInfos = new MFFGeneratorInfoRecord?[]
        {
            new MFFGeneratorInfoRecordBuilder()
                .SrcLocatorType(GeneratorConstants.TypeLocator.MFFDynamicLinqTypeLocator)
                .SrcLocatorInfo(serializedLocatorInfo)
                .Build()
        }.ToImmutableArray();

        var matchingType = new MFFTypeSymbolRecordBuilder()
            .ContainingNamespace(containingNamespace)
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
        var cut = new MFFDynamicLinqTypeLocatorTestClass(_mockSerializer.Object);

        //Act
        var actual = cut.ExposedGetMatchedTypes(genInfos, srcSymbols, cancellationToken);

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
    public void GetMatchedTypes_WhenTypes2ExcludeProvidedSerialized_DoesNotReturnExcludedType ()
    {
        //Arrange

        var containingNamespace = "Test1Space";
        var linqWhere = $"ContainingNamespace == \"{ containingNamespace }\"";
        var type2Exclude = "TestSpace.Type2Exclude";
        var serializedLocatorInfo = 
$$"""
{
    linqWhere: "{{linqWhere}}",
    Types2Exclude: ["{{type2Exclude}}"]
}
""";

        _mockSerializer.Setup(x => x.DeserializeObject<MFFDynamicLinqTypeLocatorInfo>(It.Is<string>(s => s == serializedLocatorInfo)))
            .Returns(new MFFDynamicLinqTypeLocatorInfo()
                {
                    LinqWhere = linqWhere,
                    Types2Exclude = new string[]{ type2Exclude }
                })
            .Verifiable(Times.Once);

        var genInfos = new MFFGeneratorInfoRecord?[]
        {
            new MFFGeneratorInfoRecordBuilder()
                .SrcLocatorType(GeneratorConstants.TypeLocator.MFFDynamicLinqTypeLocator)
                .SrcLocatorInfo(serializedLocatorInfo)
                .Build()
        }.ToImmutableArray();

        var matchingType = new MFFTypeSymbolRecordBuilder()
            .ContainingNamespace(containingNamespace)
            .Build();

        var excludedType = new MFFTypeSymbolRecordBuilder()
            .ContainingNamespace(containingNamespace)
            .FullyQualifiedName(type2Exclude)
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
        var cut = new MFFDynamicLinqTypeLocatorTestClass(_mockSerializer.Object);

        //Act
        var actual = cut.ExposedGetMatchedTypes(genInfos, srcSymbols, cancellationToken);

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
