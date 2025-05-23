using System.Reflection;

using MavFiFoundation.SourceGenerators.Testing;
using Microsoft.CodeAnalysis.Testing;

namespace MavFiFoundation.SourceGenerators.IntegrationTests;

public class MFFGeneratorTests
{

    [Theory]
    [ClassData(typeof(MFFGeneratorTestDataGenerator))]
    public static async Task GenerateSources_AddsExpectedSources(
        MFFGeneratorTestData scenario)
    {
        await GeneratorTestAssistants.RunAsync<MFFGenerator>(
            scenario.Sources,
            scenario.AdditionalFiles,
            scenario.GeneratedSources,
            new Assembly[]{
                typeof(MFFGeneratorBase).Assembly,
                typeof(SourceGenerators.MFFGenerateSourceAttribute).Assembly,
                typeof(SourceGenerators.TestSupport.EmbeddedResourceHelper).Assembly
            },
            Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(true);
    }
}
