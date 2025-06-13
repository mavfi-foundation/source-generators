// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using Microsoft.CodeAnalysis.Diagnostics;

namespace MavFiFoundation.SourceGenerators.Testing;

public abstract class MFFAnalyzerTestDataProviderBase : MFFTestDataProviderBase, IEnumerable<object[]>
{

    #region AnalyzerTestDataBuilder<TData>

    /// <summary>
    /// Provides a builder for constructing test data instances of type <typeparamref name="TData"/> for use in analyzer testing.
    /// </summary>
    /// <typeparam name="TData">
    /// The type of test data to build. Must inherit from <see cref="MFFAnalyzerTestData"/> and have a parameterless constructor.
    /// </typeparam>
    protected class AnalyzerTestDataBuilder<TData> : TestDataBuilder<TData>
        where TData : MFFAnalyzerTestData, new()
    {
        private readonly ICollection<DiagnosticAnalyzer> _curAnalyzers =
            new HashSet<DiagnosticAnalyzer>();

        private readonly ICollection<DiagnosticAnalyzer> _allAnalyzers =
            new HashSet<DiagnosticAnalyzer>();

        public override void BeginTest(string testName)
        {
            base.BeginTest(testName);
            _curAnalyzers.Clear();
        }

        public void AddAnalyzer(DiagnosticAnalyzer analyzer)
        {
            _curAnalyzers.Add(analyzer);
            _allAnalyzers.Add(analyzer);
        }

        protected override void AddTestData(TData testData)
        {
            testData.Analyzers = _curAnalyzers.ToArray();
        }

        protected override void AddAllTestData(TData testData)
        {
            testData.Analyzers = _allAnalyzers.ToArray();
        }
    }

    #endregion
}

