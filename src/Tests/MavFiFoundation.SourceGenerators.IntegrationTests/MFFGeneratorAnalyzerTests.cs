using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.CSharp.Testing;
using MavFiFoundation.SourceGenerators.Testing;

namespace MavFiFoundation.SourceGenerators.IntegrationTests;

using VerifyCS = CSharpAnalyzerVerifier<
    MFFGeneratorAnalyzer,
    DefaultVerifier>;

public class MFFGeneratorAnalyzerTests
{

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


    [SkippableFact]
    public async Task TestIfStatement()
    {

#if NET481
        SkipOnUnSupportedPlatforms();
#endif
        var diagnosticResult = new DiagnosticResult("MY0002", DiagnosticSeverity.Warning)
            .WithMessage("Blocks should use braces")
            .WithLocation(5, 9);

        var source =
@"class C
{
    void M()
    {
        if (true)
            return;
    }
}";

        await AnalyzerTestAssistants.RunAsync<MFFGeneratorAnalyzer>(
            source,
            diagnosticResult).ConfigureAwait(true);
    }

    [SkippableFact]
    public Task TestIfWithBlock()
    {

#if NET481
        SkipOnUnSupportedPlatforms();
#endif

        return VerifyCS.VerifyAnalyzerAsync(
@"class C
{
    void M()
    {
        if (true)
        {
            return;
        }
    }
}");
    }
}

