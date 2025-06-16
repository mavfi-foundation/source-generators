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
using Microsoft.CodeAnalysis.CodeFixes;

namespace MavFiFoundation.SourceGenerators.Testing;

public static class CodeFixTestAssistants
{
    /// <inheritdoc cref="CodeFixTestAssistants.RunAsync(IEnumerable{DiagnosticAnalyzer}, IEnumerable{CodeFixProvider}, IEnumerable{string}, string, IEnumerable{DiagnosticResult}?, IEnumerable{ValueTuple{string, string}}?, IEnumerable{Assembly}?, int?, int?, string?)" />
    /// <param name="analyzer">The analyzers to check.</param>
    /// <param name="codeFixProvider">The code fix provider.</param>
    /// <param name="source">Source file to pass to the analyzer.</param>
    public static async Task RunAsync(
        DiagnosticAnalyzer analyzer,
        CodeFixProvider codeFixProvider,
        string source,
        string fixedSource,
        IEnumerable<DiagnosticResult>? expectedDiagnostics = null,
        IEnumerable<(string, string)>? additionalFiles = null,
        IEnumerable<Assembly>? additionalReferences = null,
        int? codeActionIndex = null,
        int? numberOfIncrementalIterations = null,
        string? codeActionEquivalenceKey = null)
    {
        var analyzers = new DiagnosticAnalyzer[] { analyzer };
        var codeFixProviders = new CodeFixProvider[] { codeFixProvider };
        var sources = new string[] { source };
        await RunAsync(
            analyzers,
            codeFixProviders,
            sources,
            fixedSource,
            expectedDiagnostics,
            additionalFiles,
            additionalReferences,
            codeActionIndex,
            numberOfIncrementalIterations,
            codeActionEquivalenceKey);
    }

    /// <summary>
    /// Runs code fixes against the provided sources and additional files.
    /// </summary>
    /// <param name="analyzers">The analyzers to check.</param>
    /// <param name="codeFixProviders">The codeFixProviders to check.</param>
    /// <param name="sources">Source files to pass to the analyzer.</param>
    /// <param name="fixedSource">The expected Source file after the fix has been applied.</param>
    /// <param name="additionalFiles">Optional. Additional files to pass to the analyzer.</param>
    /// <param name="expectedDiagnostics">Optional. Expected diagnostics.</param>
    /// <param name="additionalReferences">Optional. Additional references to make available to the analyzer.</param>

    /// <param name="codeActionIndex">
    /// Optional. Index of the code action to apply. See <see cref="CodeActionTest{TVerifier}.CodeActionIndex" /> for additional information.
    /// </param>
    /// <param name="numberOfIncrementalIterations">
    /// Optional. number of code fix iterations expected during code fix testing. 
    /// See <see cref="CodeFixTest{TVerifier}.NumberOfIncrementalIterations" /> for additional information.
    /// </param>
    /// <param name="codeActionEquivalenceKey">
    /// Optional. The Microsoft.CodeAnalysis.CodeActions.CodeAction.EquivalenceKey of the code action to apply. See 
    /// <see cref="CodeActionTest{TVerifier}.CodeActionEquivalenceKey" /> for additional information. 
    /// </param>

    public static async Task RunAsync(
        IEnumerable<DiagnosticAnalyzer> analyzers,
        IEnumerable<CodeFixProvider> codeFixProviders,
        IEnumerable<string> sources,
        string fixedSource,
        IEnumerable<DiagnosticResult>? expectedDiagnostics = null,
        IEnumerable<(string, string)>? additionalFiles = null,
        IEnumerable<Assembly>? additionalReferences = null,
        int? codeActionIndex = null,
        int? numberOfIncrementalIterations = null,
        string? codeActionEquivalenceKey = null)
    {
        var test = new CSharpCodeFixVerifier.Test(analyzers, codeFixProviders)
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

        test.CodeActionIndex = codeActionIndex;
        test.NumberOfIncrementalIterations = numberOfIncrementalIterations;
        test.CodeActionEquivalenceKey = codeActionEquivalenceKey;
        test.FixedCode = fixedSource;

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