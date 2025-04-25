using Microsoft.CodeAnalysis;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.ResourceLoaders;

namespace MavFiFoundation.SourceGenerators.GeneratorTriggers;

public interface IMFFGeneratorTrigger : IMFFGeneratorPlugin
{
    IncrementalValuesProvider<MFFGeneratorInfoRecord?> GetGeneratorInfosProvider (
        IncrementalGeneratorInitializationContext genContext,
        IncrementalValuesProvider<MFFTypeSymbolSources> allTypes, 
        IncrementalValuesProvider<MFFResourceRecord> allResources,
        IEnumerable<IMFFResourceLoader> resourceLoaders);
}
