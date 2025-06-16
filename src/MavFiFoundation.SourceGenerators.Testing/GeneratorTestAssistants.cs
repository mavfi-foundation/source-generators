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
using Microsoft.CodeAnalysis;
using System.Reflection;

namespace MavFiFoundation.SourceGenerators.Testing;

/// <summary>
/// Provides utility methods to assist with testing C# source generators using the <see cref="CSharpIncrementalSourceGeneratorVerifier{T}"/>.
/// </summary>
public static class GeneratorTestAssistants
{
    /// <inheritdoc cref="GeneratorTestAssistants.RunAsync{T}(IEnumerable{string}, IEnumerable{ValueTuple{Type, string, string}}, IEnumerable{ValueTuple{string, string}}?, IEnumerable{Assembly}?, IEnumerable{DiagnosticResult}?)" />
    /// <param name="code">The source code string to be provided to the generator.</param>
    /// <param name="generatedSource">A tuple containing the type, file name, and source code of the expected generated source.</param>
	public static async Task RunAsync<T>(string code,
        (Type, string, string) generatedSource,
        IEnumerable<(string, string)>? additionalFiles = null,
        IEnumerable<Assembly>? additionalReferences = null,
        IEnumerable<DiagnosticResult>? expectedDiagnostics = null) where T : IIncrementalGenerator, new()
    {
        var sources = new List<string>() { code };
        var generatedSources = new List<(Type, string, string)>() { generatedSource };
        await RunAsync<T>(sources, generatedSources, additionalFiles, additionalReferences, expectedDiagnostics);
    }

    /// <inheritdoc cref="GeneratorTestAssistants.RunAsync{T}(IEnumerable{string}, IEnumerable{ValueTuple{Type, string, string}}, IEnumerable{ValueTuple{string, string}}?, IEnumerable{Assembly}?, IEnumerable{DiagnosticResult}?)" />
    /// <inheritdoc cref="GeneratorTestAssistants.RunAsync{T}(string, ValueTuple{Type, string, string}, IEnumerable{ValueTuple{string, string}}?, IEnumerable{Assembly}?, IEnumerable{DiagnosticResult}?)" />
	public static async Task RunAsync<T>(string code,
        IEnumerable<(Type, string, string)> generatedSources,
        IEnumerable<(string, string)>? additionalFiles = null,
        IEnumerable<Assembly>? additionalReferences = null,
        IEnumerable<DiagnosticResult>? expectedDiagnostics = null) where T : IIncrementalGenerator, new()
    {
        var sources = new List<string>() { code };
        await RunAsync<T>(sources, generatedSources, additionalFiles, additionalReferences, expectedDiagnostics);
    }

    /// <inheritdoc cref="GeneratorTestAssistants.RunAsync{T}(IEnumerable{string}, IEnumerable{ValueTuple{Type, string, string}}, IEnumerable{ValueTuple{string, string}}?, IEnumerable{Assembly}?, IEnumerable{DiagnosticResult}?)" />
    /// <inheritdoc cref="GeneratorTestAssistants.RunAsync{T}(string, ValueTuple{Type, string, string}, IEnumerable{ValueTuple{string, string}}?, IEnumerable{Assembly}?, IEnumerable{DiagnosticResult}?)" />
    public static async Task RunAsync<T>(IEnumerable<string> sources,
        (Type, string, string) generatedSource,
        IEnumerable<(string, string)>? additionalFiles = null,
        IEnumerable<Assembly>? additionalReferences = null,
        IEnumerable<DiagnosticResult>? expectedDiagnostics = null) where T : IIncrementalGenerator, new()
    {
        var generatedSources = new List<(Type, string, string)>() { generatedSource };
        await RunAsync<T>(sources, generatedSources, additionalFiles, additionalReferences, expectedDiagnostics);
    }

    /// <summary>
    /// Executes the source generator test asynchronously with the specified input sources and a single expected generated source.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the incremental generator to test. Must implement <see cref="IIncrementalGenerator"/> and have a parameterless constructor.
    /// </typeparam>
    /// <param name="sources">
    /// The collection of input source code strings to be provided to the generator.
    /// </param>
    /// <param name="generatedSource">
    /// The collection of tuple containing the type, file name, and source code of the expected generated sources.
    /// </param>
    /// <param name="additionalFiles">
    /// Optional. A collection of additional files (as tuples of file name and content) to include in the test environment.
    /// </param>
    /// <param name="additionalReferences">
    /// Optional. A collection of additional assemblies to reference during the test.
    /// </param>
    /// <param name="expectedDiagnostics">
    /// Optional. A collection of expected diagnostics that should be produced by the generator.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> representing the asynchronous operation.
    /// </returns>
    public static async Task RunAsync<T>(IEnumerable<string> sources,
        IEnumerable<(Type, string, string)> generatedSources,
        IEnumerable<(string, string)>? additionalFiles = null,
        IEnumerable<Assembly>? additionalReferences = null,
        IEnumerable<DiagnosticResult>? expectedDiagnostics = null) where T : IIncrementalGenerator, new()
    {
        var test = new CSharpIncrementalSourceGeneratorVerifier<T>.Test
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

        if (additionalFiles is not null)
        {
            foreach (var additionalFile in additionalFiles)
            {
                test.TestState.AdditionalFiles.Add(additionalFile);
            }
        }

        foreach (var generatedSource in generatedSources)
        {
            test.TestState.GeneratedSources.Add((generatedSource.Item1, generatedSource.Item2, generatedSource.Item3.Replace("\r\n", Environment.NewLine)));
        }

        if (additionalReferences is not null)
        {
            foreach (var additionalReference in additionalReferences)
            {
                test.TestState.AdditionalReferences.Add(additionalReference);
            }
        }

        if (expectedDiagnostics is not null)
        {
            test.TestState.ExpectedDiagnostics.AddRange(expectedDiagnostics);
        }

        await test.RunAsync().ConfigureAwait(false);
    }
}