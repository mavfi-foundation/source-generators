// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using System.Collections.Immutable;

using MavFiFoundation.SourceGenerators.GeneratorTriggers;
using MavFiFoundation.SourceGenerators.Models;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MavFiFoundation.SourceGenerators;

/// <summary>
/// The base class that all <see cref="IMFFGeneratorPlugin"/> implementations SHOULD implement.
/// </summary>
public class MFFGeneratorPluginBase : IMFFGeneratorPlugin
{
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
    public virtual IEnumerable<Diagnostic>? Validate(MFFAnalysisContext context,
        MFFGeneratorInfoModel genInfo,
        IMFFGeneratorTrigger generatorTrigger)
    {
        // No supported diagnostics by default.
        // This method should be overridden by derived classes to add supported diagnostics. 
        return null;
    }
}
