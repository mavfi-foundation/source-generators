using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using MavFiFoundation.SourceGenerators.Testing;
using MavFiFoundation.SourceGenerators.TestSupport;
using System.Reflection;
using MavFiFoundation.SourceGenerators.GeneratorTriggers;

namespace MavFiFoundation.SourceGenerators.IntegrationTests;

public class MFFGeneratorAnalyzerTests
{

    #region Test Support Methods
#if NET481
    private void SkipOnUnSupportedPlatforms()
    {
        var skip = false;

        switch (Environment.OSVersion.Platform)
        {
            case PlatformID.Unix:
            case PlatformID.MacOSX:
                skip = true;
                break;
        }

        Skip.If(skip, "Skipping test for unsupported platform.");
    }
#endif

    #endregion

    #region Test Methods

    [SkippableFact]
    public async Task MFFAttributeTrigger_InvalidTypeLocator_Fix()
    {
#if NET481
        SkipOnUnSupportedPlatforms();
#endif

        var diagnosticResult = new DiagnosticResult(
            MFFAttributeGeneratorTrigger.InvalidTypeLocatorDiagnosticId,
            DiagnosticSeverity.Error)
            .WithMessage(MFFAttributeGeneratorTrigger.InvalidTypeLocatorMessageFormat)
            .WithSpan(6, 21, 6, 41);


        var source = EmbeddedResourceHelper.ReadEmbeddedSource(
                    "MFFAttributeGeneratorTrigger_InvalidTypeLocator.cs",
                    EmbeddedResourceHelper.EmbeddedResourceType.Code);

        var fixedSource = EmbeddedResourceHelper.ReadEmbeddedSource(
                    "MFFAttributeGeneratorTrigger_InvalidTypeLocator.cs",
                    EmbeddedResourceHelper.EmbeddedResourceType.Code)
                        .Replace("\"InvalidTypeLocator\"","\"MFFIncludedTypeLocator\"");

        await CodeFixTestAssistants.RunAsync(
            [new MFFGeneratorAnalyzer()],
            [new MFFGeneratorCodeFix()],
            [source], fixedSource, [diagnosticResult], null, new Assembly[]{
                typeof(MFFGeneratorAnalyzerBase).Assembly,
                typeof(MFFGenerateSourceAttribute).Assembly,
                typeof(EmbeddedResourceHelper).Assembly },
                numberOfIncrementalIterations: 1,
                codeActionEquivalenceKey: "Use \"MFFIncludedTypeLocator\" literal").ConfigureAwait(true);
    }

    [SkippableFact]
    public async Task MFFAttributeTrigger_InvalidTypeLocator()
    {

#if NET481
        SkipOnUnSupportedPlatforms();
#endif
        var diagnosticResult = new DiagnosticResult(
            MFFAttributeGeneratorTrigger.InvalidTypeLocatorDiagnosticId,
            DiagnosticSeverity.Error)
            .WithMessage(MFFAttributeGeneratorTrigger.InvalidTypeLocatorMessageFormat)
            .WithSpan(6, 21, 6, 41);

        var source = EmbeddedResourceHelper.ReadEmbeddedSource(
                    "MFFAttributeGeneratorTrigger_InvalidTypeLocator.cs",
                    EmbeddedResourceHelper.EmbeddedResourceType.Code);

        await AnalyzerTestAssistants.RunAsync<MFFGeneratorAnalyzer>(
            source, diagnosticResult, null, new Assembly[]{
                typeof(MFFGeneratorAnalyzerBase).Assembly,
                typeof(MFFGenerateSourceAttribute).Assembly,
                typeof(EmbeddedResourceHelper).Assembly }).ConfigureAwait(true);
    }

    [SkippableFact]
    public async Task MFFAttributeTrigger_NoOutputs()
    {

#if NET481
        SkipOnUnSupportedPlatforms();
#endif
        var diagnosticResult = new DiagnosticResult(
            MFFAttributeGeneratorTrigger.NoOutputsDiagnosticId,
            DiagnosticSeverity.Warning)
            .WithMessage(MFFAttributeGeneratorTrigger.NoOutputsMessageFormat)
            .WithSpan(6, 20, 12, 3);

        var source = EmbeddedResourceHelper.ReadEmbeddedSource(
                    "MFFAttributeGeneratorTrigger_NoOutputs.cs",
                    EmbeddedResourceHelper.EmbeddedResourceType.Code);

        await AnalyzerTestAssistants.RunAsync<MFFGeneratorAnalyzer>(
            source, diagnosticResult, null, [
                typeof(MFFGeneratorAnalyzerBase).Assembly,
                typeof(MFFGenerateSourceAttribute).Assembly,
                typeof(EmbeddedResourceHelper).Assembly ]).ConfigureAwait(true);
    }

    #endregion
}
