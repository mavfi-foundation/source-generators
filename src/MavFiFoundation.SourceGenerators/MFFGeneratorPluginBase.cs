// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.GeneratorTriggers;
using MavFiFoundation.SourceGenerators.Models;
using Microsoft.CodeAnalysis;

namespace MavFiFoundation.SourceGenerators;

/// <summary>
/// The base class that all <see cref="IMFFGeneratorPlugin"/> implementations SHOULD implement.
/// </summary>
public class MFFGeneratorPluginBase : IMFFGeneratorPlugin
{
    /// <summary>
    /// Default Diagnostic Discription
    /// </summary>
    protected const string DiagnosticCategory = "SourceGeneration";

    /// <inheritdoc/>
    public string Name { get; private set; }

    /// <summary>
    /// Constructor for <see cref="MFFGeneratorPluginBase"/>
    /// </summary>
    /// <param name="name">The name used to identify the plugin.</param>
    public MFFGeneratorPluginBase(string name)
    {
        Name = name;
    }

    /// <inheritdoc/>
    public virtual void AddSupportedAnalyzerDiagnostics(ImmutableArray<DiagnosticDescriptor>.Builder supportedDiagnoticsBuilder)
    {
        // No supported diagnostics by default.
        // This method should be overridden by derived classes to add supported diagnostics. 
    }

    /// <inheritdoc/>
    public virtual void AddFixableDiagnosticIds(ImmutableArray<string>.Builder fixableDiagnosticIdsBuilder)
    {
        // No supported code fix diagnostic ids by default.
        // This method should be overridden by derived classes to add supported code fix diagnostic ids. 
    }


    /// <inheritdoc/>
    public virtual IEnumerable<Diagnostic>? Validate(MFFAnalysisContext context,
        object source,
        MFFGeneratorInfoModel genInfo,
        IMFFGeneratorTrigger generatorTrigger)
    {
        // No supported diagnostics by default.
        // This method should be overridden by derived classes to add supported diagnostics. 
        return null;
    }

    /// <inheritdoc/>
    public virtual IEnumerable<MFFCodeAction>? GetCodeActions(string diagnosticId, SyntaxNode syntaxNode)
    {
        // No code actions by default.
        // This method should be overridden by derived classes to provide code actions.
        return null;
    }

    /// <summary>
    /// Appends a <see cref="Diagnostic"/> to an existing <see cref="IEnumerable<Diagnostic>"/> 
    /// or creates a new <see cref="IEnumerable<Diagnostic>" with the provided dianostic.
    /// </summary>
    /// <param name="diagnostics">The diagnostics to append to.</param>
    /// <param name="diagnostic">The diagnostic to be added.</param>
    protected void AddDiagnostic(ref IEnumerable<Diagnostic>? diagnostics, Diagnostic diagnostic)
    {
        if (diagnostics is null)
        {
            diagnostics = [diagnostic];
        }
        else
        {
            diagnostics = diagnostics.Append(diagnostic);
        }
    }

    /// Adds a <see cref="MFFCodeAction"/> to the specified collection of code actions.
    /// </summary>
    /// <param name="codeActions">
    /// A reference to the collection of <see cref="MFFCodeAction"/> instances to which the new action will be added.
    /// If <c>null</c>, a new collection will be created containing the specified <paramref name="codeAction"/>.
    /// </param>
    /// <param name="codeAction">
    /// The <see cref="MFFCodeAction"/> to add to the collection.
    /// </param>
    protected void AddCodeAction(ref IEnumerable<MFFCodeAction>? codeActions, MFFCodeAction codeAction)
    {
        if (codeActions is null)
        {
            codeActions = [codeAction];
        }
        else
        {
            codeActions = codeActions.Append(codeAction);
        }
    }

    /// Adds the specified code actions to the existing collection of code actions.
    /// </summary>
    /// <param name="codeActions">
    /// A reference to the current collection of <see cref="MFFCodeAction"/> objects. 
    /// If <c>null</c>, it will be set to <paramref name="codeActions2Add"/>.
    /// </param>
    /// <param name="codeActions2Add">
    /// The collection of <see cref="MFFCodeAction"/> objects to add to <paramref name="codeActions"/>.
    /// </param>
    protected void AddCodeActions(ref IEnumerable<MFFCodeAction>? codeActions, IEnumerable<MFFCodeAction> codeActions2Add)
    {
        if (codeActions is null)
        {
            codeActions = codeActions2Add;
        }
        else
        {
            codeActions = codeActions.Concat(codeActions2Add);
        }
    }

}
