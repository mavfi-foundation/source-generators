// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

/**********************************************************************************
*
* Original code based on AutoDeconstruct generator created by Jason Bock
* and published in 'Writing Code to Generate Code in C#' article located at
* https://www.codemag.com/Article/2305061/Writing-Code-to-Generate-Code-in-C#
* AutoDestruct code was retrieved from https://github.com/JasonBock/AutoDeconstruct
*
***********************************************************************************/

using Microsoft.CodeAnalysis.Testing;
using System.Reflection;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MavFiFoundation.SourceGenerators.Testing;

public static class AnalyzerTestAssistants
{
    /// <inheritdoc cref="AnalyzerTestAssistants.RunAsync(IEnumerable{DiagnosticAnalyzer}, IEnumerable{string}, IEnumerable{DiagnosticResult}?, IEnumerable{ValueTuple{string, string}}?, IEnumerable{Assembly}?)"/>
    /// <param name="analyzer">The analyzers to check.</param>
    /// <param name="source">Source file to pass to the analyzer.</param>
    /// <param name="expectedDiagnostic">Expected diagnostic.</param>
    public static async Task RunAsync(
        DiagnosticAnalyzer analyzer,
        string source,
        DiagnosticResult? expectedDiagnostic = null,
        IEnumerable<(string, string)>? additionalFiles = null,
        IEnumerable<Assembly>? additionalReferences = null)
    {
        IEnumerable<DiagnosticResult>? expectedDiagnostics =
            (expectedDiagnostic is not null && expectedDiagnostic.HasValue) ?
                [expectedDiagnostic.Value] :
                null;

        var analyzers = new DiagnosticAnalyzer[] { analyzer };
        var sources = new string[]  { source };
        await RunAsync(analyzers, sources, expectedDiagnostics, additionalFiles, additionalReferences);
    }

    /// <inheritdoc cref="AnalyzerTestAssistants.RunAsync(DiagnosticAnalyzer, string, DiagnosticResult?, IEnumerable{ValueTuple{string, string}}?, IEnumerable{Assembly}?)"/>
    /// <typeparam name="TAnalyzer">The analyzer type to test.</typeparam>
    public static async Task RunAsync<TAnalyzer>(
        string source,
        DiagnosticResult? expectedDiagnostic = null,
        IEnumerable<(string, string)>? additionalFiles = null,
        IEnumerable<Assembly>? additionalReferences = null) where TAnalyzer : DiagnosticAnalyzer, new()
    {
        IEnumerable<DiagnosticResult>? expectedDiagnostics =
            (expectedDiagnostic is not null && expectedDiagnostic.HasValue) ?
                [expectedDiagnostic.Value] :
                null;

        var analyzers = new DiagnosticAnalyzer[] { new TAnalyzer() };
        var sources = new string[] { source };
        await RunAsync(analyzers, sources, expectedDiagnostics, additionalFiles, additionalReferences);
    } 

    /// <inheritdoc cref="AnalyzerTestAssistants.RunAsync(DiagnosticAnalyzer, string, DiagnosticResult?, IEnumerable{ValueTuple{string, string}}?, IEnumerable{Assembly}?)"/>
    public static async Task RunAsync(
        DiagnosticAnalyzer analyzer,
        string source,
        IEnumerable<DiagnosticResult>? expectedDiagnostics = null,
        IEnumerable<(string, string)>? additionalFiles = null,
        IEnumerable<Assembly>? additionalReferences = null)
    {
        var analyzers = new DiagnosticAnalyzer[] { analyzer };
        var sources = new string[] { source };
        await RunAsync(analyzers, sources, expectedDiagnostics, additionalFiles, additionalReferences);
    }

    /// <inheritdoc cref="AnalyzerTestAssistants.RunAsync(DiagnosticAnalyzer, string, DiagnosticResult?, IEnumerable{ValueTuple{string, string}}?, IEnumerable{Assembly}?)"/>
    public static async Task RunAsync(
        DiagnosticAnalyzer analyzer,
        IEnumerable<string> sources,
        IEnumerable<DiagnosticResult>? expectedDiagnostics = null,
        IEnumerable<(string, string)>? additionalFiles = null,
        IEnumerable<Assembly>? additionalReferences = null)
    {
        var analyzers = new DiagnosticAnalyzer[] { analyzer };
        await RunAsync(analyzers, sources, expectedDiagnostics, additionalFiles, additionalReferences);
    }

    /// <inheritdoc cref="AnalyzerTestAssistants.RunAsync(DiagnosticAnalyzer, string, DiagnosticResult?, IEnumerable{ValueTuple{string, string}}?, IEnumerable{Assembly}?)"/>
    public static async Task RunAsync(
        IEnumerable<DiagnosticAnalyzer> analyzers,
        string source,
        IEnumerable<DiagnosticResult>? expectedDiagnostics = null,
        IEnumerable<(string, string)>? additionalFiles = null,
        IEnumerable<Assembly>? additionalReferences = null)
    {
        var sources = new string[]  { source };
        await RunAsync(analyzers, sources, expectedDiagnostics, additionalFiles, additionalReferences);
    }

    /// <summary>
    /// Runs analyzers against the provided sources and additional files.
    /// </summary>
    /// <param name="analyzers">The analyzers to check.</param>
    /// <param name="sources">Source files to pass to the analyzer.</param>
    /// <param name="additionalFiles">Optional. Additional files to pass to the analyzer.</param>
    /// <param name="expectedDiagnostics">Optional. Expected diagnostics.</param>
    /// <param name="additionalReferences">Optional. Additional references to make available to the analyzer.</param>
    public static async Task RunAsync(
        IEnumerable<DiagnosticAnalyzer> analyzers,
        IEnumerable<string> sources,
        IEnumerable<DiagnosticResult>? expectedDiagnostics = null,
        IEnumerable<(string, string)>? additionalFiles = null,
        IEnumerable<Assembly>? additionalReferences = null)
    {
        var test = new CSharpAnalyzerVerifier.Test(analyzers)
        {
#if NET8_0
			ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
#elif NET9_0
			ReferenceAssemblies = ReferenceAssemblies.Net.Net90,
#else
            ReferenceAssemblies = ReferenceAssemblies.NetStandard.NetStandard20,
#endif
        };

        foreach (var source in sources)
        {
            test.TestState.Sources.Add(source);
        }

        if (expectedDiagnostics is not null)
        {
            test.TestState.ExpectedDiagnostics.AddRange(expectedDiagnostics);
        }

        if (additionalFiles is not null)
        {
            foreach (var additionalFile in additionalFiles)
            {
                test.TestState.AdditionalFiles.Add(additionalFile);
            }
        }

        if (additionalReferences is not null)
        {
            foreach (var additionalReference in additionalReferences)
            {
                test.TestState.AdditionalReferences.Add(additionalReference);
            }

        }

        await test.RunAsync().ConfigureAwait(false);
    }
}