// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using System.Collections.Immutable;

using MavFiFoundation.SourceGenerators.GeneratorTriggers;
using MavFiFoundation.SourceGenerators.Models;

using Microsoft.CodeAnalysis;

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
    /// Adds the supported code fix diagnotic ids used by code fix implementations.
    /// </summary>
    /// <param name="fixableDiagnosticIdsBuilder">The <see cref="ImmutableArray<string>.Builder"/>  to add supported code fix diagnostics ids to.</param>
    void AddFixableDiagnosticIds(ImmutableArray<string>.Builder fixableDiagnosticIdsBuilder);

    /// <summary>
    /// Validates the content of trigger files.
    /// </summary>
    /// <param name="context">The analysis context.</param>
    /// <param name="source">The <see cref="ISymbol"/> or <see cref="AdditionalText"/> to be validated.</param>
    /// <param name="genInfo">The generator configuration to validate.</param>
    /// <param name="generatorTrigger">The generator trigger that created <paramref name="genInfo"/>.</param>
    /// <returns><see cref="IEnumerable<Diagnostic>"/> with failed validation or null when no validations failed. </returns>
    IEnumerable<Diagnostic>? Validate(MFFAnalysisContext context,
        object source,
        MFFGeneratorInfoModel genInfo,
        IMFFGeneratorTrigger generatorTrigger);


    /// <summary>
    /// Retrieves a collection of <see cref="MFFCodeAction"/> instances that provide code fixes or refactorings
    /// for the specified diagnostic and syntax node.
    /// </summary>
    /// <param name="diagnosticId">The identifier of the diagnostic to address.</param>
    /// <param name="syntaxNode">The <see cref="SyntaxNode"/> associated with the diagnostic.</param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of <see cref="MFFCodeAction"/> objects representing available code actions.
    /// </returns>
    IEnumerable<MFFCodeAction>? GetCodeActions(string diagnosticId, SyntaxNode syntaxNode);
}
