// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using Microsoft.CodeAnalysis.CodeFixes;

namespace MavFiFoundation.SourceGenerators.Testing;

/// <summary>
/// Represents test data for code fix testing scenarios
/// </summary>
public class MFFCodeFixTestData : MFFAnalyzerTestData
{

    /// <summary>
    /// Gets or sets the collection of <see cref="CodeFixProvider"/> instances to be used for code fix testing.
    /// </summary>
    public IEnumerable<CodeFixProvider> CodeFixProviders { get; set; } = new HashSet<CodeFixProvider>();

    public string FixedSource { get; set; } = string.Empty;

    public int? CodeActionIndex { get; set; }

    public int? NumberOfIncrementalIterations { get; set; }

    public string? CodeActionEquivalenceKey { get; set; }
}
