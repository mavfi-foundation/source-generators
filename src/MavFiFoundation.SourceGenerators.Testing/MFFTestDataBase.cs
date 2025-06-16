// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using System.Reflection;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;

namespace MavFiFoundation.SourceGenerators.Testing;

/// <summary>
/// Represents the base class for test data used in MavFi Foundation source generator testing.
/// </summary>
public abstract class MFFTestDataBase
{
    /// <summary>
    /// Gets or sets the scenario name or description for the test case.
    /// </summary>
    public string Scenario { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the collection of source code strings to be included in the test.
    /// </summary>
    public IEnumerable<string> Sources { get; set; } = new HashSet<string>();

    /// <summary>
    /// Gets or sets the collection of expected diagnostic results for the test.
    /// </summary>
    public IEnumerable<DiagnosticResult>? ExpectedDiagnostics { get; set; }

    /// <summary>
    /// Gets or sets the collection of additional files to be included in the test, represented as tuples of file name and content.
    /// </summary>
    public IEnumerable<(string, string)>? AdditionalFiles { get; set; }

    /// <summary>
    /// Gets or sets the collection of additional assembly references to be included in the test.
    /// </summary>
    public IEnumerable<Assembly>? AdditionalReferences { get; set; }

    /// <summary>
    /// Returns a string that represents the current test scenario.
    /// </summary>
    /// <returns>A string containing the scenario name or description.</returns>
    public override string ToString()
    {
        return $"\"{Scenario}\"";
    }
}
