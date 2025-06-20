using MavFiFoundation.SourceGenerators.GeneratorTriggers;
using MavFiFoundation.SourceGenerators.Testing;
using MavFiFoundation.SourceGenerators.TestSupport;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;

namespace MavFiFoundation.SourceGenerators.IntegrationTests;

public class MFFAnalyzerTestDataGenerator : MFFAnalyzerTestDataProviderBase
{

    public MFFAnalyzerTestDataGenerator()
    {
        var testDataBuilder = new AnalyzerTestDataBuilder<MFFAnalyzerXUnitTestData>();

         // NothingIn_NothingOut
        testDataBuilder.BeginTest(
            "NothingIn_NothingOut");

        testDataBuilder.AddSource(string.Empty);

        _data.Add(testDataBuilder.BuildTestData());
#if NET481
        if (!Helpers.ShouldSkipOnUnSupportedPlatforms())
        {
#endif

            // MFFAttributeTrigger_InvalidTypeLocator
            testDataBuilder.BeginTest(
                "MFFAttributeTrigger_InvalidTypeLocator");

            var sourceCounts = testDataBuilder.AddSource(
                EmbeddedResourceHelper.ReadEmbeddedSource(
                        "MFFAttributeGeneratorTrigger_InvalidTypeLocator.cs",
                        EmbeddedResourceHelper.EmbeddedResourceType.Code));

            testDataBuilder.AddExpectedDiagnostic(
                new DiagnosticResult(
                    MFFAttributeGeneratorTrigger.InvalidTypeLocatorDiagnosticId,
                    DiagnosticSeverity.Error)
                    .WithMessage(MFFAttributeGeneratorTrigger.InvalidTypeLocatorMessageFormat)
                    .WithSpan(6, 21, 6, 41),
                new DiagnosticResult(
                    MFFAttributeGeneratorTrigger.InvalidTypeLocatorDiagnosticId,
                    DiagnosticSeverity.Error)
                    .WithMessage(MFFAttributeGeneratorTrigger.InvalidTypeLocatorMessageFormat)
                    .WithSpan($"/0/Test{sourceCounts.AllSrcsCount - 1}.cs", 6, 21, 6, 41));

            _data.Add(testDataBuilder.BuildTestData());

            // MFFAttributeTrigger_NoOutputs
            testDataBuilder.BeginTest(
                "MFFAttributeTrigger_NoOutputs");

            sourceCounts = testDataBuilder.AddSource(
                EmbeddedResourceHelper.ReadEmbeddedSource(
                        "MFFAttributeGeneratorTrigger_NoOutputs.cs",
                        EmbeddedResourceHelper.EmbeddedResourceType.Code));

            testDataBuilder.AddExpectedDiagnostic(
                new DiagnosticResult(
                    MFFAttributeGeneratorTrigger.NoOutputsDiagnosticId,
                    DiagnosticSeverity.Warning)
                    .WithMessage(MFFAttributeGeneratorTrigger.NoOutputsMessageFormat)
                    .WithSpan(6, 20, 12, 3),
                new DiagnosticResult(
                    MFFAttributeGeneratorTrigger.NoOutputsDiagnosticId,
                    DiagnosticSeverity.Warning)
                    .WithMessage(MFFAttributeGeneratorTrigger.NoOutputsMessageFormat)
                    .WithSpan($"/0/Test{sourceCounts.AllSrcsCount - 1}.cs", 6, 20, 12, 3));

            _data.Add(testDataBuilder.BuildTestData());

#if NET481
        }
#endif
        // All
        _data.Add(testDataBuilder.BuildAllTestData());
    }
}

