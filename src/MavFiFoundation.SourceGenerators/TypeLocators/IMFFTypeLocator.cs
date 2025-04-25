using Microsoft.CodeAnalysis;
using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.TypeLocators;

public interface IMFFTypeLocator : IMFFGeneratorPlugin
{
    IncrementalValuesProvider<MFFGeneratorInfoWithSrcTypesRecord?> GetTypeSymbolsProvider (
        IncrementalGeneratorInitializationContext genContext,
        IncrementalValuesProvider<MFFGeneratorInfoRecord?> genInfos,
        IncrementalValuesProvider<MFFTypeSymbolSources> allTypes
    );
}
