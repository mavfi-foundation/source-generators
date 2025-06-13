// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using Microsoft.CodeAnalysis.CodeFixes;

namespace MavFiFoundation.SourceGenerators.Testing;

public abstract class MFFCodeFixTestDataProviderBase : MFFAnalyzerTestDataProviderBase, IEnumerable<object[]>
{

    #region CodeFixTestDataBuilder<TData>

    /// <summary>
    /// Provides a builder for constructing test data instances of type <typeparamref name="TData"/> for use in code fix testing.
    /// </summary>
    /// <typeparam name="TData">
    /// The type of test data to build. Must inherit from <see cref="MFFAnalyzerTestData"/> and have a parameterless constructor.
    /// </typeparam>
    protected class CodeFixTestDataBuilder<TData> : AnalyzerTestDataBuilder<TData>
        where TData : MFFCodeFixTestData, new()
    {
        private string _curFixedSource = string.Empty;
        private int? _curCodeActionIndex = null;

        private int? _curNumberOfIncrementalIterations = null;

        private string? _curCodeActionEquivalenceKey = null;

        private readonly ICollection<CodeFixProvider> _curCodeFixProviders =
            new HashSet<CodeFixProvider>();

        private readonly ICollection<CodeFixProvider> _allCodeFixProviders =
            new HashSet<CodeFixProvider>();

        public override void BeginTest(string testName)
        {
            base.BeginTest(testName);
            _curCodeFixProviders.Clear();
        }

        public void BeginTest(
            string testName,
            string fixedSource,
            int? codeActionIndex = null,
            int? numberOfIncrementalIterations = null,
            string? codeActionEquivalenceKey = null)
        {
            BeginTest(testName);
            _curFixedSource = fixedSource;
            _curCodeActionIndex = codeActionIndex;
            _curNumberOfIncrementalIterations = codeActionIndex;
            _curCodeActionEquivalenceKey = codeActionEquivalenceKey;
        }

        public void AddCodeFixProvider(CodeFixProvider provider)
        {
            _curCodeFixProviders.Add(provider);
            _allCodeFixProviders.Add(provider);
        }

        protected override void AddTestData(TData testData)
        {
            testData.CodeFixProviders = _curCodeFixProviders.ToArray();
            testData.FixedSource = _curFixedSource;
            testData.CodeActionIndex = _curCodeActionIndex;
            testData.NumberOfIncrementalIterations = _curNumberOfIncrementalIterations;
            testData.CodeActionEquivalenceKey = _curCodeActionEquivalenceKey;
        }

        protected override void AddAllTestData(TData testData)
        {
            testData.CodeFixProviders = _allCodeFixProviders.ToArray();
            testData.FixedSource = _curFixedSource;
            testData.CodeActionIndex = _curCodeActionIndex;
            testData.NumberOfIncrementalIterations = _curNumberOfIncrementalIterations;
            testData.CodeActionEquivalenceKey = _curCodeActionEquivalenceKey;
        }
    }

    #endregion
}

