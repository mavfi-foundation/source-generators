// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using System.Collections.Immutable;

using MavFiFoundation.SourceGenerators.GeneratorTriggers;
using MavFiFoundation.SourceGenerators.Models;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MavFiFoundation.SourceGenerators;

/// <summary>
/// Defines the interface to be used by all generator plugins.
/// </summary>
public interface IMFFGeneratorPlugin
{
    /// <summary>
    /// Gets the name used to identity the plugin.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Adds the supported diagnotics used by analyzer implementations.
    /// </summary>
    /// <param name="supportedDiagnoticsBuilder">The <see cref="ImmutableArray<DiagnosticDescriptor>.Builder"/>  to add supported diagnostics to.</param>
    void AddSupportedAnalyzerDiagnostics(ImmutableArray<DiagnosticDescriptor>.Builder supportedDiagnoticsBuilder);

   /// <summary>
    /// Validates the content of trigger files.
    /// </summary>
    /// <param name="context">The analysis context.</param>
    /// <param name="genInfo">The generator configuration to validate.</param>
    /// <param name="generatorTrigger">The generator trigger that created <paramref name="genInfo"/>.</param>
    /// <returns><see cref="IEnumerable<Diagnostic>"/> with failed validation or null when no validations failed. </returns>
    IEnumerable<Diagnostic>? Validate(MFFAnalysisContext context,
        MFFGeneratorInfoModel genInfo,
        IMFFGeneratorTrigger generatorTrigger);

}
