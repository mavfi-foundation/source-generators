// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using MavFiFoundation.SourceGenerators.Testing;
using MavFiFoundation.SourceGenerators.TestSupport;
using System.Reflection;

namespace MavFiFoundation.SourceGenerators.IntegrationTests;

public class MFFGeneratorCodeFixTests
{
    [Theory]
    [ClassData(typeof(MFFCodeFixTestDataGenerator))]
    public async Task GenerateSources_AppliesExpectedCodeFix(
        MFFCodeFixTestData scenario)
    {
        var additionalReferences = new Assembly[]{
                typeof(MFFGeneratorAnalyzerBase).Assembly,
                typeof(MFFGenerateSourceAttribute).Assembly,
                typeof(EmbeddedResourceHelper).Assembly
        };

        if (scenario.AdditionalReferences is not null)
        {
            additionalReferences = additionalReferences.Concat(scenario.AdditionalReferences).ToArray();
        }

        await CodeFixTestAssistants.RunAsync(
            [new MFFGeneratorAnalyzer()],
            [new MFFGeneratorCodeFix()],
            scenario.Sources,
            scenario.FixedSource,
            scenario.ExpectedDiagnostics,
            scenario.AdditionalFiles,
            additionalReferences,
            scenario.CodeActionIndex,
            scenario.NumberOfIncrementalIterations,
            scenario.CodeActionEquivalenceKey).ConfigureAwait(true);
    }
}
