using MavFiFoundation.SourceGenerators.GeneratorTriggers;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.ResourceLoaders;

using System.Collections.Immutable;

namespace MavFiFoundation.SourceGenerators.UnitTests.GeneratorTriggers;

public class MFFAttributeGeneratorTriggerTests : MFFAttributeGeneratorTrigger
{

    [Fact]
    public void GetTypesWithAttribute_NothingIn_NothingOut ()
    {
        //Arrange
        var srcSymbols = Array.Empty<MFFTypeSymbolSources>().ToImmutableArray();
        var resources = Array.Empty<MFFResourceRecord>().ToImmutableArray();
        var loaders = Array.Empty<IMFFResourceLoader>();
        var cancellationToken = new CancellationToken();

        //Act
        var mut = GetTypesWithAttribute(srcSymbols, resources, loaders, cancellationToken);

        //Assert
        mut.Length.Should().Be(0);
    }
}
