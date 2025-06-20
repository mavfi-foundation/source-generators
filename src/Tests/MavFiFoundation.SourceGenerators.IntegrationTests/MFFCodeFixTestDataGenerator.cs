// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using MavFiFoundation.SourceGenerators.GeneratorTriggers;
using MavFiFoundation.SourceGenerators.Testing;
using MavFiFoundation.SourceGenerators.TestSupport;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;

namespace MavFiFoundation.SourceGenerators.IntegrationTests;

public class MFFCodeFixTestDataGenerator : MFFCodeFixTestDataProviderBase
{

    public MFFCodeFixTestDataGenerator()
    {
        var testDataBuilder = new CodeFixTestDataBuilder<MFFCodeFixXUnitTestData>();

        // NothingIn_NothingOut
        testDataBuilder.BeginTest(
            "NothingIn_NothingOut");

        testDataBuilder.AddSource(string.Empty);

        _data.Add(testDataBuilder.BuildTestData());

#if NET481
        if (!Helpers.ShouldSkipOnUnSupportedPlatforms())
        {
#endif
        // MFFAttributeTrigger_InvalidTypeLocator_Fix
        var source = EmbeddedResourceHelper.ReadEmbeddedSource(
                        "MFFAttributeGeneratorTrigger_InvalidTypeLocator.cs",
                        EmbeddedResourceHelper.EmbeddedResourceType.Code);

        var fixedSource = source.Replace("\"InvalidTypeLocator\"", "\"MFFIncludedTypeLocator\"");

        testDataBuilder.BeginTest(
                "MFFAttributeTrigger_InvalidTypeLocator_Fix",
                fixedSource,
                numberOfIncrementalIterations: 1,
                codeActionEquivalenceKey: "Use \"MFFIncludedTypeLocator\" literal");

        var sourceCounts = testDataBuilder.AddSource(source);

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

#if NET481
        }
#endif
    }
}

