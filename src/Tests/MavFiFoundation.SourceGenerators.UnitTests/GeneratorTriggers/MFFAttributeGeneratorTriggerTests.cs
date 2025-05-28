using MavFiFoundation.SourceGenerators.GeneratorTriggers;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.ResourceLoaders;
using MavFiFoundation.SourceGenerators.Serializers;
using MavFiFoundation.SourceGenerators.TypeLocators;
using System.Collections.Immutable;
using Moq.Protected;
using MavFiFoundation.SourceGenerators.Testing.Models;

namespace MavFiFoundation.SourceGenerators.UnitTests.GeneratorTriggers;

public class MFFAttributeGeneratorTriggerTests
{

    private readonly Mock<IMFFGeneratorPluginsProvider> _mockPluginsProvider;
    private readonly Mock<IMFFSerializer> _mockSerializer;

    public MFFAttributeGeneratorTriggerTests()
    {
        _mockPluginsProvider = new Mock<IMFFGeneratorPluginsProvider>(MockBehavior.Strict);
        _mockSerializer = new Mock<IMFFSerializer>(MockBehavior.Strict);
        _mockSerializer.Setup(x => x.DeserializeObject<List<MFFBuilderModel>?>(It.IsAny<String>()))
            .Returns(new List<MFFBuilderModel>());
    }

    [Fact]
    public void GetTypesWithAttribute_NothingIn_NothingOut ()
    {
        //Arrange
        var srcSymbols = Array.Empty<MFFTypeSymbolSources>().ToImmutableArray();
        var resources = Array.Empty<MFFResourceRecord>().ToImmutableArray();
        var loaders = Array.Empty<IMFFResourceLoader>();
        var cancellationToken = new CancellationToken();
        var cut = new MFFAttributeGeneratorTriggerTestClass(
            _mockPluginsProvider.Object, _mockSerializer.Object);

        //Act
        var actual = cut.ExposedGetTypesWithAttribute(srcSymbols, resources, loaders, cancellationToken);

        //Assert
        actual.Should().HaveCount(0);
    }

    [Fact]
    public void GetTypesWithAttribute_WithScrLocatorInfo_ReturnsPopulatedArray ()
    {
        //Arrange
        var matchContainingNamespace = "CorrectClassNameSpace";

        var srcSymbols = new MFFTypeSymbolSources[] 
        {
            new MFFTypeSymbolSourcesBuilder()
                .AddType(new MFFTypeSymbolRecordBuilder()
                    .ContainingNamespace(matchContainingNamespace)
                    .AddAttribute(new MFFAttributeDataRecordBuilder()
                        .Name(MFFAttributeGeneratorTrigger.DEFAULT_ATTRIBUTE_NAME)
                        .AddProperty(new MFFAttributePropertyRecordBuilder()
                                .Name(MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_SRCLOCATORTYPE)
                                .Value(MFFAttributeTypeLocator.DEFAULT_NAME)
                                .Build())
                        .AddProperty(new MFFAttributePropertyRecordBuilder()
                                .Name(MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_SRCLOCATORINFO)
                                .Value("SomeAttribute")
                                .Build())
                        .AddProperty(new MFFAttributePropertyRecordBuilder()
                                .Name(MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_USESYMBOLFORLOCATORINFO)
                                .Value(false)
                                .Build())
                        .AddProperty(new MFFAttributePropertyRecordBuilder()
                                .Name(MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_OUTPUTINFO)
                                .Value("OutputInfo")
                                .Build())
                        .Build())
                    .Build())
                .AddType(new MFFTypeSymbolRecordBuilder().Build())
                .Build(),
        }.ToImmutableArray();
        
        var resources = Array.Empty<MFFResourceRecord>().ToImmutableArray();
        var loaders = Array.Empty<IMFFResourceLoader>();
        var cancellationToken = new CancellationToken();
        var cut = new MFFAttributeGeneratorTriggerTestClass(
            _mockPluginsProvider.Object, _mockSerializer.Object);

        //Act
        var actual = cut.ExposedGetTypesWithAttribute(srcSymbols, resources, loaders, cancellationToken);

        //Assert
        actual.Should().HaveCount(1);
        var actual_0 = actual[0];
        actual_0.Should().NotBeNull();
        actual_0.ContainingNamespace.Should().Be(matchContainingNamespace);
    }
    [Fact]
    public void GetTypesWithAttribute_WithoutScrLocatorInfo_ReturnsPopulatedArray_WithScrLocatorInfoSet ()
    {
        //Arrange
        var matchContainingNamespace = "CorrectClassNameSpace";

        var srcSymbols = new MFFTypeSymbolSources[] 
        {
            new MFFTypeSymbolSourcesBuilder()
                .AddType(new MFFTypeSymbolRecordBuilder()
                    .ContainingNamespace(matchContainingNamespace)
                    .AddAttribute(new MFFAttributeDataRecordBuilder()
                        .Name(MFFAttributeGeneratorTrigger.DEFAULT_ATTRIBUTE_NAME)
                        .AddProperty(new MFFAttributePropertyRecordBuilder()
                                .Name(MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_SRCLOCATORTYPE)
                                .Value(MFFAttributeTypeLocator.DEFAULT_NAME)
                                .Build())
                        .AddProperty(new MFFAttributePropertyRecordBuilder()
                                .Name(MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_USESYMBOLFORLOCATORINFO)
                                .Value(true)
                                .Build())
                        .AddProperty(new MFFAttributePropertyRecordBuilder()
                                .Name(MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_OUTPUTINFO)
                                .Value("OutputInfo")
                                .Build())
                        .Build())
                    .Build())
                .AddType(new MFFTypeSymbolRecordBuilder().Build())
                .Build()
        }.ToImmutableArray();
        
        var resources = Array.Empty<MFFResourceRecord>().ToImmutableArray();
        var loaders = Array.Empty<IMFFResourceLoader>();
        var cancellationToken = new CancellationToken();
        var cut = new MFFAttributeGeneratorTriggerTestClass(
            _mockPluginsProvider.Object, _mockSerializer.Object);

        //Act
        var actual = cut.ExposedGetTypesWithAttribute(srcSymbols, resources, loaders, cancellationToken);

        //Assert
        actual.Should().HaveCount(1);
        var actual_0 = actual[0];
        actual_0.Should().NotBeNull();
        actual_0.ContainingNamespace.Should().Be(matchContainingNamespace);
        actual_0.SrcLocatorInfo.Should().BeSameAs(srcSymbols[0].Types[0]);
    }

    [Fact]
    public void GetTypesWithAttribute_AttemptsToLoadResources ()
    {
        //Arrange
        var matchContainingNamespace = "CorrectClassNameSpace";

        var srcSymbols = new MFFTypeSymbolSources[] 
        {
            new MFFTypeSymbolSourcesBuilder()
                .AddType(new MFFTypeSymbolRecordBuilder()
                    .ContainingNamespace(matchContainingNamespace)
                    .AddAttribute(new MFFAttributeDataRecordBuilder()
                        .Name(MFFAttributeGeneratorTrigger.DEFAULT_ATTRIBUTE_NAME)
                        .AddProperty(new MFFAttributePropertyRecordBuilder()
                                .Name(MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_SRCLOCATORTYPE)
                                .Value(MFFAttributeTypeLocator.DEFAULT_NAME)
                                .Build())
                        .AddProperty(new MFFAttributePropertyRecordBuilder()
                                .Name(MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_USESYMBOLFORLOCATORINFO)
                                .Value(true)
                                .Build())
                        .AddProperty(new MFFAttributePropertyRecordBuilder()
                                .Name(MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_OUTPUTINFO)
                                .Value("OutputInfo")
                                .Build())
                        .Build())
                    .Build())
                .Build()
        }.ToImmutableArray();
        
        var resources = Array.Empty<MFFResourceRecord>().ToImmutableArray();
        var loaders = Array.Empty<IMFFResourceLoader>();
        var cancellationToken = new CancellationToken();
        var cutMock = new Mock<MFFAttributeGeneratorTriggerTestClass>(
            _mockPluginsProvider.Object, _mockSerializer.Object);
        cutMock.Protected().Setup("LoadResources",
            ItExpr.IsAny<MFFGeneratorInfoModel>(),
            ItExpr.IsAny<ImmutableArray<MFFResourceRecord>>(),
            ItExpr.IsAny<IEnumerable<IMFFResourceLoader>>(),
            ItExpr.IsAny<CancellationToken>()).Verifiable(Times.Once);

        //Act
        cutMock.Object.ExposedGetTypesWithAttribute(srcSymbols, resources, loaders, cancellationToken);

        //Assert
        cutMock.VerifyAll();
    }

}
