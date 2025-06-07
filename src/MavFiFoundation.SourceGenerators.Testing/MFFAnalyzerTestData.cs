// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using System.Reflection;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;

namespace MavFiFoundation.SourceGenerators.Testing;

/// <summary>
/// Represents test data for analyzer testing scenarios
/// </summary>
public class MFFAnalyzerTestData : MFFTestDataBase
{
    /// <summary>
    /// Gets or sets the collection of diagnostic analyzers to be used in the test.
    /// </summary>
    public IEnumerable<DiagnosticAnalyzer> Analyzers { get; set; } = new HashSet<DiagnosticAnalyzer>();

}
