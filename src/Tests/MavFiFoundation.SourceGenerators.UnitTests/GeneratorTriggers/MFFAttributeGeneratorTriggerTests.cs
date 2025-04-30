using MavFiFoundation.SourceGenerators.Builders;
using MavFiFoundation.SourceGenerators.GeneratorTriggers;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.ResourceLoaders;
using MavFiFoundation.SourceGenerators.Serializers;
using MavFiFoundation.SourceGenerators.TypeLocators;

using System.Collections.Immutable;
using System.ComponentModel;

namespace MavFiFoundation.SourceGenerators.UnitTests.GeneratorTriggers;

public class MFFAttributeGeneratorTriggerTests : MFFAttributeGeneratorTrigger
{

    private readonly Mock<IMFFResourceLoader> _mockResourceLoader;

    public MFFAttributeGeneratorTriggerTests() : base(new MFFJsonSerializer())
    {
        _mockResourceLoader = new Mock<IMFFResourceLoader>(MockBehavior.Strict);
    }

    [Fact]
    public void GetTypesWithAttribute_NothingIn_NothingOut ()
    {
        //Arrange
        var srcSymbols = Array.Empty<MFFTypeSymbolSources>().ToImmutableArray();
        var resources = Array.Empty<MFFResourceRecord>().ToImmutableArray();
        var loaders = Array.Empty<IMFFResourceLoader>();
        var cancellationToken = new CancellationToken();

        //Act
        var actual = GetTypesWithAttribute(srcSymbols, resources, loaders, cancellationToken);

        //Assert
        actual.Length.Should().Be(0);
    }

    [Fact]
    public void GetTypesWithAttribute_ReturnsPopulatedArray ()
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
                                DEFAULT_ATTRIBUTE_NAME,
                                new MFFAttributePropertyRecord[]
                                {
                                    new MFFAttributePropertyRecord
                                    (
                                        CTOR_ARG_SRCLOCATORTYPE,
                                        MFFAttributeTypeLocator.DEFAULT_NAME,
                                        MFFAttributePropertyLocationType.Constructor
                                    ),
                                    new MFFAttributePropertyRecord
                                    (
                                        CTOR_ARG_SRCLOCATORINFO,
                                        "SomeAttribute",
                                        MFFAttributePropertyLocationType.Constructor
                                    ),
                                    new MFFAttributePropertyRecord
                                    (
                                        CTOR_ARG_OUTPUTINFO,
 """
[
	{
		"FileNameBuilderInfo": "{{ srcType.Name }}_Generated.g.cs", 
""" +
$"\n		\"SourceBuilderType\": \"{ MFFScribanBuilder.DEFAULT_NAME }\",\n" +
"""
		"SourceBuilderInfo": "#nullable enable\n\n
""" +
"""
public partial class {{ srcType.Name }}_Generated\n{\n\n}"
""" + 
"""
	}
]
""",
                                       MFFAttributePropertyLocationType.Constructor
                                    )
                                }.ToImmutableArray()
                            )
                        }.ToImmutableArray()
                    )
                }.ToImmutableArray()
            ),
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
        
        var resources = Array.Empty<MFFResourceRecord>().ToImmutableArray();
        var loaders = Array.Empty<IMFFResourceLoader>();
        var cancellationToken = new CancellationToken();

        //Act
        var actual = GetTypesWithAttribute(srcSymbols, resources, loaders, cancellationToken);

        //Assert
        actual.Length.Should().Be(1);
        actual[0].Should().NotBeNull();
        actual[0]!.ContainingNamespace.Should().Be(matchContainingNamespace);
    }
}
