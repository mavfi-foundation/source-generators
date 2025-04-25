using MavFiFoundation.SourceGenerators.Models;
using Microsoft.CodeAnalysis;

namespace MavFiFoundation.SourceGenerators;

public interface IMFFGeneratorHelper
{
    IncrementalValuesProvider<MFFTypeSymbolSources>
        GetAllTypesProvider(IncrementalGeneratorInitializationContext initContext);

    void ProcessNamespace(INamespaceSymbol ns2Process,
        Action<INamedTypeSymbol> typeProcessor,
        CancellationToken cancellationToken);

    IncrementalValuesProvider<MFFResourceRecord>
        GetAllResourcesProvider(IncrementalGeneratorInitializationContext initContext);

    IncrementalValuesProvider<MFFGeneratorInfoRecord?> GetGenerateConstantsProvider(
        IncrementalGeneratorInitializationContext initContext,
        IMFFGeneratorPluginsProvider pluginsProvider);
}
