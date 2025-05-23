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

public static class GeneratorTestAssistants
{
	public static async Task RunAsync<T>(string code,
		IEnumerable<(Type, string, string)> generatedSources,
        IEnumerable<Assembly> additionalReferences,
		IEnumerable<DiagnosticResult> expectedDiagnostics) where T : IIncrementalGenerator, new()
	{
		var sources = new HashSet<string>(){ code };
		await RunAsync<T>(sources, generatedSources, additionalReferences, expectedDiagnostics); 
	}

	public static async Task RunAsync<T>(IEnumerable<string> sources,
		IEnumerable<(Type, string, string)> generatedSources,
        IEnumerable<Assembly> additionalReferences,
		IEnumerable<DiagnosticResult> expectedDiagnostics) where T : IIncrementalGenerator, new()
	{
		var additionalFiles = new HashSet<(string, string)>();
		await RunAsync<T>(
            sources, additionalFiles, generatedSources, additionalReferences, expectedDiagnostics); 		
	}

	public static async Task RunAsync<T>(string code,
		IEnumerable<(string, string)> additionalFiles,
		IEnumerable<(Type, string, string)> generatedSources,
        IEnumerable<Assembly> additionalReferences,
		IEnumerable<DiagnosticResult> expectedDiagnostics) where T : IIncrementalGenerator, new()
	{
		var sources = new HashSet<string>(){ code };
		await RunAsync<T>(
            sources, additionalFiles, generatedSources, additionalReferences, expectedDiagnostics); 		
	}

	public static async Task RunAsync<T>(IEnumerable<string> sources,
		IEnumerable<(string, string)> additionalFiles,
		IEnumerable<(Type, string, string)> generatedSources,
        IEnumerable<Assembly> additionalReferences,
		IEnumerable<DiagnosticResult> expectedDiagnostics) where T : IIncrementalGenerator, new()
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

		foreach (var additionalFile in additionalFiles)
		{
			test.TestState.AdditionalFiles.Add(additionalFile);
		}

		foreach (var generatedSource in generatedSources)
		{
			test.TestState.GeneratedSources.Add((generatedSource.Item1, generatedSource.Item2, generatedSource.Item3.Replace("\r\n", Environment.NewLine)));
		}

        foreach (var additionalReference in additionalReferences)
        {
            test.TestState.AdditionalReferences.Add(additionalReference);
        }

		test.TestState.ExpectedDiagnostics.AddRange(expectedDiagnostics);
		await test.RunAsync().ConfigureAwait(false);
	}
}