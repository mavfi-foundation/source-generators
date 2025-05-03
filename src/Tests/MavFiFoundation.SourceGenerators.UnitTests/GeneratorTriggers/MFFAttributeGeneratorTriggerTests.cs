using MavFiFoundation.SourceGenerators.GeneratorTriggers;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.ResourceLoaders;
using MavFiFoundation.SourceGenerators.Serializers;
using MavFiFoundation.SourceGenerators.TypeLocators;
using System.Collections.Immutable;
using Moq.Protected;

namespace MavFiFoundation.SourceGenerators.UnitTests.GeneratorTriggers;

public class MFFAttributeGeneratorTriggerTests
{

    private readonly Mock<IMFFSerializer> _mockSerializer;

    public MFFAttributeGeneratorTriggerTests()
    {
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
        var cut = new MFFAttributeGeneratorTriggerTestClass(_mockSerializer.Object);

        //Act
        var actual = cut.ExposedGetTypesWithAttribute(srcSymbols, resources, loaders, cancellationToken);

        //Assert
        actual.Length.Should().Be(0);
    }

    [Fact]
    public void GetTypesWithAttribute_WithScrLocatorInfo_ReturnsPopulatedArray ()
    {
        //Arrange
        var matchContainingNamespace = "CorrectClassNameSpace";

        var srcSymbols = new MFFTypeSymbolSources[] 
        {
            new MFFTypeSymbolSources
            (
                MFFGeneratorConstants.Generator.COMPILING_PROJECT,
                new MFFTypeSymbolRecord[]
                {
                    new MFFTypeSymbolRecord
                    (
                        matchContainingNamespace,
                        "Name",
                        "GenericParameters",
                        "FullyQualifiedName",
                        "Contraints",
                        false,
                        new MFFPropertySymbolRecord[]{}.ToImmutableArray(),
                        new MFFAttributeDataRecord[]
                        {
                            new MFFAttributeDataRecord
                            (
                                MFFAttributeGeneratorTrigger.DEFAULT_ATTRIBUTE_NAME,
                                new MFFAttributePropertyRecord[]
                                {
                                    new MFFAttributePropertyRecord
                                    (
                                        MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_SRCLOCATORTYPE,
                                        MFFAttributeTypeLocator.DEFAULT_NAME,
                                        MFFAttributePropertyLocationType.Constructor
                                    ),
                                    new MFFAttributePropertyRecord
                                    (
                                        MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_SRCLOCATORINFO,
                                        "SomeAttribute",
                                        MFFAttributePropertyLocationType.Constructor
                                    ),
                                    new MFFAttributePropertyRecord
                                    (
                                        MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_USESYMBOLFORLOCATORINFO,
                                        false,
                                        MFFAttributePropertyLocationType.Constructor
                                    ),
                                    new MFFAttributePropertyRecord
                                    (
                                        MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_OUTPUTINFO,
                                        "OutputInfo",
                                        MFFAttributePropertyLocationType.Constructor
                                    )
                                }.ToImmutableArray()
                            )
                        }.ToImmutableArray()
                    ),
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
        
        var resources = Array.Empty<MFFResourceRecord>().ToImmutableArray();
        var loaders = Array.Empty<IMFFResourceLoader>();
        var cancellationToken = new CancellationToken();
        var cut = new MFFAttributeGeneratorTriggerTestClass(_mockSerializer.Object);

        //Act
        var actual = cut.ExposedGetTypesWithAttribute(srcSymbols, resources, loaders, cancellationToken);

        //Assert
        actual.Length.Should().Be(1);
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
            new MFFTypeSymbolSources
            (
                MFFGeneratorConstants.Generator.COMPILING_PROJECT,
                new MFFTypeSymbolRecord[]
                {
                    new MFFTypeSymbolRecord
                    (
                        matchContainingNamespace,
                        "Name",
                        "GenericParameters",
                        "FullyQualifiedName",
                        "Contraints",
                        false,
                        new MFFPropertySymbolRecord[]{}.ToImmutableArray(),
                        new MFFAttributeDataRecord[]
                        {
                            new MFFAttributeDataRecord
                            (
                                MFFAttributeGeneratorTrigger.DEFAULT_ATTRIBUTE_NAME,
                                new MFFAttributePropertyRecord[]
                                {
                                    new MFFAttributePropertyRecord
                                    (
                                        MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_SRCLOCATORTYPE,
                                        MFFAttributeTypeLocator.DEFAULT_NAME,
                                        MFFAttributePropertyLocationType.Constructor
                                    ),
                                    new MFFAttributePropertyRecord
                                    (
                                        MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_USESYMBOLFORLOCATORINFO,
                                        true,
                                        MFFAttributePropertyLocationType.Constructor
                                    ),
                                    new MFFAttributePropertyRecord
                                    (
                                        MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_OUTPUTINFO,
                                        "OutputInfo",
                                        MFFAttributePropertyLocationType.Constructor
                                    )
                                }.ToImmutableArray()
                            )
                        }.ToImmutableArray()
                    ),
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
        
        var resources = Array.Empty<MFFResourceRecord>().ToImmutableArray();
        var loaders = Array.Empty<IMFFResourceLoader>();
        var cancellationToken = new CancellationToken();
        var cut = new MFFAttributeGeneratorTriggerTestClass(_mockSerializer.Object);

        //Act
        var actual = cut.ExposedGetTypesWithAttribute(srcSymbols, resources, loaders, cancellationToken);

        //Assert
        actual.Length.Should().Be(1);
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
            new MFFTypeSymbolSources
            (
                MFFGeneratorConstants.Generator.COMPILING_PROJECT,
                new MFFTypeSymbolRecord[]
                {
                    new MFFTypeSymbolRecord
                    (
                        matchContainingNamespace,
                        "Name",
                        "GenericParameters",
                        "FullyQualifiedName",
                        "Contraints",
                        false,
                        new MFFPropertySymbolRecord[]{}.ToImmutableArray(),
                        new MFFAttributeDataRecord[]
                        {
                            new MFFAttributeDataRecord
                            (
                                MFFAttributeGeneratorTrigger.DEFAULT_ATTRIBUTE_NAME,
                                new MFFAttributePropertyRecord[]
                                {
                                    new MFFAttributePropertyRecord
                                    (
                                        MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_SRCLOCATORTYPE,
                                        MFFAttributeTypeLocator.DEFAULT_NAME,
                                        MFFAttributePropertyLocationType.Constructor
                                    ),
                                    new MFFAttributePropertyRecord
                                    (
                                        MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_USESYMBOLFORLOCATORINFO,
                                        true,
                                        MFFAttributePropertyLocationType.Constructor
                                    ),
                                    new MFFAttributePropertyRecord
                                    (
                                        MFFAttributeGeneratorTriggerTestClass.EXPOSED_CTOR_ARG_OUTPUTINFO,
                                        "OutputInfo",
                                        MFFAttributePropertyLocationType.Constructor
                                    )
                                }.ToImmutableArray()
                            )
                        }.ToImmutableArray()
                    )
                }.ToImmutableArray()
            )
        }.ToImmutableArray();
        
        var resources = Array.Empty<MFFResourceRecord>().ToImmutableArray();
        var loaders = Array.Empty<IMFFResourceLoader>();
        var cancellationToken = new CancellationToken();
        var cutMock = new Mock<MFFAttributeGeneratorTriggerTestClass>(_mockSerializer.Object);
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
