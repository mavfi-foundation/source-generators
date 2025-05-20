// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using MavFiFoundation.SourceGenerators.Models;
using Microsoft.CodeAnalysis;

namespace MavFiFoundation.SourceGenerators;

/// <summary>
/// Defines helper methods for <see cref="MFFGeneratorBase" /> instances.
/// </summary>
public interface IMFFGeneratorHelper
{
    /// <summary>
    /// Creates a value provider that returns all available types.
    /// </summary>
    /// <param name="initContext">Initialization context</param>
    /// <returns>The created <see cref="IncrementalValuesProvider{TValues}" /></returns>
    public IncrementalValuesProvider<MFFTypeSymbolSources>
        GetAllTypesProvider(IncrementalGeneratorInitializationContext initContext);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ns2Process"></param>
    /// <param name="typeProcessor"></param>
    /// <param name="cancellationToken">Cancellation token to check.</param>
    void ProcessNamespace(INamespaceSymbol ns2Process,
        Action<INamedTypeSymbol> typeProcessor,
        CancellationToken cancellationToken);

    IncrementalValuesProvider<MFFResourceRecord>
        GetAllResourcesProvider(IncrementalGeneratorInitializationContext initContext);

    IncrementalValuesProvider<MFFGeneratorInfoRecord?> GetGenerateConstantsProvider(
        IncrementalGeneratorInitializationContext initContext,
        IMFFGeneratorPluginsProvider pluginsProvider);
}
