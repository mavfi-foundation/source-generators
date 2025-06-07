// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MavFiFoundation.SourceGenerators.Testing;

/// <summary>
/// Provides a test harness for verifying C# code fixes using Roslyn analyzers and code fix providers.
/// </summary>
public static partial class CSharpCodeFixVerifier
{
#pragma warning disable CA1034 // Nested types should not be visible
    /// <summary>
    /// Represents a test case for verifying code fixes in C# using specified analyzers and code fix providers.
    /// </summary>
    public class Test : CodeFixTest<DefaultVerifier>
#pragma warning restore CA1034 // Nested types should not be visible
    {
        #region Private/Protected Fields

        private static readonly LanguageVersion DefaultLanguageVersion =
            Enum.TryParse("Default", out LanguageVersion version) ? version : LanguageVersion.CSharp6;

        private IEnumerable<DiagnosticAnalyzer> _analyzers;
        private IEnumerable<CodeFixProvider> _codeFixProviders;

        #endregion

        #region Constructors

        public Test()
        {
            _analyzers = Array.Empty<DiagnosticAnalyzer>();
            _codeFixProviders = Array.Empty<CodeFixProvider>();
        }

        /// <param name="analyzers">Collection of <see cref="DiagnosticAnalyzer"/> being tested.</param>
        /// <param name="codeFixProviders">Collection of <see cref="CodeFixProvider"/> being tested.</param>
        public Test(
            IEnumerable<DiagnosticAnalyzer> analyzers,
            IEnumerable<CodeFixProvider> codeFixProviders)
        {
            _analyzers = analyzers;
            _codeFixProviders = codeFixProviders;
        }

        #endregion

        #region CodeFixTest Implementation

        /// <inheritdoc/>
        public override string Language => LanguageNames.CSharp;

        /// <inheritdoc/>
        protected override string DefaultFileExt => "cs";

        /// <inheritdoc/>
        protected override CompilationOptions CreateCompilationOptions()
            => new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, allowUnsafe: true);

        /// <inheritdoc/>
        protected override ParseOptions CreateParseOptions()
            => new CSharpParseOptions(DefaultLanguageVersion, DocumentationMode.Diagnose);

        /// <inheritdoc/>
        protected override IEnumerable<DiagnosticAnalyzer> GetDiagnosticAnalyzers() => _analyzers;

        /// <inheritdoc/>
        protected override IEnumerable<CodeFixProvider> GetCodeFixProviders() => _codeFixProviders;

        /// <inheritdoc/>
        public override Type SyntaxKindType => typeof(SyntaxKind);

        /// <summary>
        /// Add an additional <see cref="DiagnosticAnalyzer" to be tested./>
        /// </summary>
        /// <param name="analyzer">The analyzer to add.</param>
        public void AddAnalyzer(DiagnosticAnalyzer analyzer)
        {
            _analyzers = _analyzers.Append(analyzer);
        }

        /// <summary>
        /// Add an additional <see cref="CodeFixProvider" to be tested./>
        /// </summary>
        /// <param name="codeFixProviders">The code fix providers to add.</param>
        public void AddCodeFixProvider(CodeFixProvider codeFixProviders)
        {
            _codeFixProviders = _codeFixProviders.Append(codeFixProviders);
        }
        
        #endregion
    }
}