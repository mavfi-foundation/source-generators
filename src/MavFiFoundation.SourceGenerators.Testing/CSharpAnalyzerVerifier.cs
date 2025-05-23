
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MavFiFoundation.SourceGenerators.Testing;

public static partial class CSharpAnalyzerVerifier
{
#pragma warning disable CA1034 // Nested types should not be visible
    public class Test : AnalyzerTest<DefaultVerifier>
#pragma warning restore CA1034 // Nested types should not be visible
    {

        private IEnumerable<DiagnosticAnalyzer> _analyzers;

        public Test()
        {
            _analyzers = Array.Empty<DiagnosticAnalyzer>();
        }

        public Test(IEnumerable<DiagnosticAnalyzer> analyzers)
        {
            _analyzers = analyzers;
        }

        public override string Language => LanguageNames.CSharp;

        protected override string DefaultFileExt => "cs";

        protected override CompilationOptions CreateCompilationOptions()
        {
            var compilerOptions = new CSharpCompilationOptions(OutputKind.NetModule);
            return compilerOptions;
        }

        protected override ParseOptions CreateParseOptions()
        {
            var parseOptions = new CSharpParseOptions();
            return parseOptions.WithLanguageVersion(LanguageVersion.Preview);
        }

        protected override IEnumerable<DiagnosticAnalyzer> GetDiagnosticAnalyzers()
        {
            return _analyzers;
        }

        public void AddAnalyzer(DiagnosticAnalyzer analyzer)
        {
            _analyzers = _analyzers.Append(analyzer);
        }
    }
}