// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.ResourceLoaders;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MavFiFoundation.SourceGenerators.GeneratorTriggers;

/// <summary>
/// Defines the interface to be used by all generator triggers.
/// </summary>
public interface IMFFGeneratorTrigger : IMFFGeneratorPlugin
{
    /// <summary>
    /// Creates a <see cref="IncrementalValuesProvider{TValues}"/> that locates and reads generator configuration information.
    /// </summary>
    /// <param name="genContext">Initialization context to use.</param>
    /// <param name="allTypes">All available types.</param>
    /// <param name="allResources">External resources used to load additional configuration information.</param>
    /// <param name="resourceLoaders">Helper instances for loading additional configuration information from external sources.</param>
    /// <returns>The created <see cref="IncrementalValuesProvider{TValues}"/></returns>
    IncrementalValuesProvider<MFFGeneratorInfoRecord?> GetGeneratorInfosProvider(
        IncrementalGeneratorInitializationContext genContext,
        IncrementalValuesProvider<MFFTypeSymbolSources> allTypes,
        IncrementalValuesProvider<MFFResourceRecord> allResources,
        IEnumerable<IMFFResourceLoader> resourceLoaders);

    /// <summary>
    /// Validates the content of trigger files.
    /// </summary>
    /// <param name="context">The analysis context.</param>
    MFFGeneratorInfoModel? ValidateAdditionalFile(AdditionalFileAnalysisContext context);

    /// <summary>
    /// Validates the content of symbol files.
    /// </summary>
    /// <inheritdoc cref="ValidateAdditionalFile(AdditionalFileAnalysisContext)" path="/param[@name='context']"/>
    MFFGeneratorInfoModel? ValidateSymbol(SymbolAnalysisContext context);
}
