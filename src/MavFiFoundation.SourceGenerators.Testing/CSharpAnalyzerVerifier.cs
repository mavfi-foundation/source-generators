// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MavFiFoundation.SourceGenerators.Testing;

/// <summary>
/// Provides utilities for verifying C# <see cref="DiagnosticAnalyzer"/> implementations.
/// </summary>
public static partial class CSharpAnalyzerVerifier
{
#pragma warning disable CA1034 // Nested types should not be visible
    /// <summary>
    /// Provides a test harness for verifying C# <see cref="DiagnosticAnalyzer"/> implementations.
    /// </summary>
    public class Test : AnalyzerTest<DefaultVerifier>
#pragma warning restore CA1034 // Nested types should not be visible
    {
        #region Private/Protected properties
        private static readonly LanguageVersion DefaultLanguageVersion =
            Enum.TryParse("Default", out LanguageVersion version) ? version : LanguageVersion.CSharp6;

        private IEnumerable<DiagnosticAnalyzer> _analyzers;

        #endregion

        #region Constructors

        public Test()
        {
            _analyzers = Array.Empty<DiagnosticAnalyzer>();
        }

        /// <param name="analyzers">Collection of <see cref="DiagnosticAnalyzer"/> to be tested.</param>
        public Test(IEnumerable<DiagnosticAnalyzer> analyzers)
        {
            _analyzers = analyzers;
        }

        #endregion

        #region AnalyzerTest Implementation

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

        /// <summary>
        /// Add an additional <see cref="DiagnosticAnalyzer" /> to be tested.
        /// </summary>
        /// <param name="analyzer">The analyzer to add.</param>
        public void AddAnalyzer(DiagnosticAnalyzer analyzer)
        {
            _analyzers = _analyzers.Append(analyzer);
        }

        #endregion
    }
}