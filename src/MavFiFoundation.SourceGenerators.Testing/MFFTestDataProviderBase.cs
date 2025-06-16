
// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using System.Collections;
using System.Reflection;

using Microsoft.CodeAnalysis.Testing;

namespace MavFiFoundation.SourceGenerators.Testing;

public abstract class MFFTestDataProviderBase : IEnumerable<object[]>
{

    #region TestDataBuilder<TData>

    /// <summary>
    /// Provides a builder for constructing test data instances of type <typeparamref name="TData"/> for use in source generator testing.
    /// </summary>
    /// <typeparam name="TData">
    /// The type of test data to build. Must inherit from <see cref="MFFTestDataBase"/> and have a parameterless constructor.
    /// </typeparam>
    protected abstract class TestDataBuilder<TData>
        where TData : MFFTestDataBase, new()
    {
        private string _curName = string.Empty;
        private readonly ICollection<string> _curSources = new HashSet<string>();
        private readonly ICollection<(string, string)> _curAdditionalFiles =
            new HashSet<(string, string)>();
        private readonly ICollection<DiagnosticResult> _curExpectedDiagnostics =
            new HashSet<DiagnosticResult>();
        private readonly ICollection<Assembly> _curAdditionalReferences =
            new HashSet<Assembly>();

        private readonly ICollection<string> _allSources = new HashSet<string>();
        private readonly ICollection<(string, string)> _allAdditionalFiles =
            new HashSet<(string, string)>();
        private readonly ICollection<DiagnosticResult> _allExpectedDiagnostics =
            new HashSet<DiagnosticResult>();
        private readonly ICollection<Assembly> _allAdditionalReferences =
            new HashSet<Assembly>();

        /// <summary>
        /// Sets the name for the current test being built and clears internal test information.
        /// </summary>
        /// <param name="testName">The test name (scenario) to begin building.</param>
        public virtual void BeginTest(string testName)
        {
            _curName = testName;
            _curSources.Clear();
            _curAdditionalFiles.Clear();
            _curExpectedDiagnostics.Clear();
            _curAdditionalReferences.Clear();
        }

        /// <summary>
        /// Adds a new source for the test.
        /// </summary>
        /// <param name="source">The source to add.</param>
        /// <returns>
        /// A Typle{int, int} with count of sources added to current test scenario and count of sources added to all test scenario,
        /// </returns>
        public (int CurSrcsCount, int AllSrcsCount) AddSource(string source)
        {
            _curSources.Add(source);
            _allSources.Add(source);

            return (CurSrcsCount: _curSources.Count, AllSrcsCount: _allSources.Count);
        }

        /// <summary>
        /// Adds an additional file to the test.
        /// </summary>
        /// <param name="additionalFile">The additional file to add.</param>
        public void AddAdditionalFile((string, string) additionalFile)
        {
            _curAdditionalFiles.Add(additionalFile);
            _allAdditionalFiles.Add(additionalFile);
        }

        /// <summary>
        /// Adds an expected diagnostic to the test.
        /// </summary>
        /// <param name="expectedDiagnostic">The expected diagnostic to add for current test scenario.</param>
        /// <param name="allExpectedDiagnostic">The expected diagnostic to add for all tests scenario.</param>
        public void AddExpectedDiagnostic(DiagnosticResult expectedDiagnostic, DiagnosticResult allExpectedDiagnostic)
        {
            _curExpectedDiagnostics.Add(expectedDiagnostic);
            _allExpectedDiagnostics.Add(allExpectedDiagnostic);
        }

        /// <summary>
        /// Adds an reference to the test.
        /// </summary>
        /// <param name="expectedDiagnostic">The reference to add.</param>
        public void AddAdditionalReferences(Assembly assembly)
        {
            _curAdditionalReferences.Add(assembly);
            _allAdditionalReferences.Add(assembly);
        }

        /// <summary>
        /// Add implementation specific data for the test.
        /// </summary>
        /// <param name="testData">The testData to add to.</param>
        protected abstract void AddTestData(TData testData);

        /// <summary>
        /// Builds the test data for the current provided test information.
        /// </summary>
        /// <returns>An object array with the built test data.</returns>
        public object[] BuildTestData()
        {
            var testData = new TData
            {
                Scenario = _curName.ToString(),
                Sources = _curSources.ToArray(),
                ExpectedDiagnostics = _curExpectedDiagnostics.ToArray(),
                AdditionalFiles = _curAdditionalFiles.ToArray(),
                AdditionalReferences = _curAdditionalReferences.ToArray()
            };

            AddTestData(testData);

            return new object[] { testData };
        }

        /// <summary>
        /// Add implementation specific data for all scenarios tests.
        /// </summary>
        /// <param name="testData">The testData to add to.</param>
        protected abstract void AddAllTestData(TData testData);

        /// <summary>
        /// Builds the test data for the all currently provided test information.
        /// </summary>
        /// <returns>An object array with the built test data.</returns>
        public object[] BuildAllTestData()
        {
            var testData = new TData
            {
                Scenario = "All_Scenarios_Together",
                Sources = _allSources.ToArray(),
                ExpectedDiagnostics = _allExpectedDiagnostics.ToArray(),
                AdditionalFiles = _allAdditionalFiles.ToArray(),
                AdditionalReferences = _allAdditionalReferences.ToArray()
            };

            AddAllTestData(testData);

            return [testData];
        }
    }

    #endregion

    #region Private/Protected properties

    protected readonly List<object[]> _data;

    #endregion

    #region Constructors

    public MFFTestDataProviderBase()
    {
        _data = new List<object[]>();
    }

    #endregion

    #region IEnumerable Implementation

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion
}

