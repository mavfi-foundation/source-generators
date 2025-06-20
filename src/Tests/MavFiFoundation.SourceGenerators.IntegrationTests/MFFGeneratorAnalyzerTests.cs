// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using MavFiFoundation.SourceGenerators.Testing;
using MavFiFoundation.SourceGenerators.TestSupport;

using System.Reflection;

using Xunit.Sdk;

namespace MavFiFoundation.SourceGenerators.IntegrationTests;

public class MFFGeneratorAnalyzerTests
{
    [Theory]
    [ClassData(typeof(MFFAnalyzerTestDataGenerator))]
    public async Task GenerateSources_AddsExpectedDiagnostic(
        MFFAnalyzerTestData scenario)
    {
#if NET481
        if (Helpers.ShouldSkipOnUnSupportedPlatforms())
        {
            Console.WriteLine($"\tWarning: Skipping test on Mono. test: {nameof(MFFGeneratorAnalyzerTests)}.{nameof(MFFGeneratorAnalyzerTests.GenerateSources_AddsExpectedDiagnostic)}.{scenario}");
        }
        else
        {
#endif
        var additionalReferences = new Assembly[]{
                typeof(MFFGeneratorAnalyzerBase).Assembly,
                typeof(MFFGenerateSourceAttribute).Assembly,
                typeof(EmbeddedResourceHelper).Assembly
        };

        if (scenario.AdditionalReferences is not null)
        {
            additionalReferences = additionalReferences.Concat(scenario.AdditionalReferences).ToArray();
        }

        await AnalyzerTestAssistants.RunAsync<MFFGeneratorAnalyzer>(
                scenario.Sources,
                scenario.ExpectedDiagnostics,
                scenario.AdditionalFiles,
                additionalReferences).ConfigureAwait(true);
#if NET481
        }
#endif

    }
}
