using Microsoft.CodeAnalysis.Testing;

namespace MavFiFoundation.SourceGenerators.IntegrationTests;

public class MFFGeneratorTests
{

    [Theory]
    [ClassData(typeof(MFFGeneratorTestDataGenerator))]
    public static async Task GenerateSources_AddsExpectedSources(
        MFFGeneratorTestData scenario)
    {
        await TestAssistants.RunAsync<MFFGenerator>(
            scenario.Sources,
            scenario.AdditionalFiles,
            scenario.GeneratedSources,
            Enumerable.Empty<DiagnosticResult>()).ConfigureAwait(true);
    }
}
