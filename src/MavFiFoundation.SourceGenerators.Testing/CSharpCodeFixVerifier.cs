using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MavFiFoundation.SourceGenerators.Testing;

public static partial class CSharpCodeFixVerifier
{
#pragma warning disable CA1034 // Nested types should not be visible
    public class Test : CodeFixTest<DefaultVerifier>
#pragma warning restore CA1034 // Nested types should not be visible
    {

        private static readonly LanguageVersion DefaultLanguageVersion =
            Enum.TryParse("Default", out LanguageVersion version) ? version : LanguageVersion.CSharp6;

        private IEnumerable<DiagnosticAnalyzer> _analyzers;
        private IEnumerable<CodeFixProvider> _codeFixProviders;

        public Test()
        {
            _analyzers = Array.Empty<DiagnosticAnalyzer>();
            _codeFixProviders = Array.Empty<CodeFixProvider>();
        }

        public Test(
            IEnumerable<DiagnosticAnalyzer> analyzers,
            IEnumerable<CodeFixProvider> codeFixProviders)
        {
            _analyzers = analyzers;
            _codeFixProviders = codeFixProviders;
        }

        public override string Language => LanguageNames.CSharp;

        protected override string DefaultFileExt => "cs";

        protected override CompilationOptions CreateCompilationOptions()
            => new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, allowUnsafe: true);

        protected override ParseOptions CreateParseOptions()
            => new CSharpParseOptions(DefaultLanguageVersion, DocumentationMode.Diagnose);

        protected override IEnumerable<DiagnosticAnalyzer> GetDiagnosticAnalyzers() => _analyzers;

        protected override IEnumerable<CodeFixProvider> GetCodeFixProviders() => _codeFixProviders;

        public override Type SyntaxKindType => typeof(SyntaxKind);
        
        public void AddAnalyzer(DiagnosticAnalyzer analyzer)
        {
            _analyzers = _analyzers.Append(analyzer);
        }

        public void AddCodeFixProvider(CodeFixProvider codeFixProviders)
        {
            _codeFixProviders = _codeFixProviders.Append(codeFixProviders);
        }
    }
}