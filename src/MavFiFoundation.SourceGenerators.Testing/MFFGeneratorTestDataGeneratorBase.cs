// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using System.Collections;

namespace MavFiFoundation.SourceGenerators.Testing;

public abstract class MFFGeneratorTestDataGeneratorBase : IEnumerable<object[]>
{

    #region TestDataBuilder<TData>

    /// <summary>
    /// Provides a builder for constructing test data instances of type <typeparamref name="TData"/> for use in source generator testing.
    /// </summary>
    /// <typeparam name="TData">
    /// The type of test data to build. Must inherit from <see cref="MFFGeneratorTestData"/> and have a parameterless constructor.
    /// </typeparam>
    /// <remarks>
    /// This builder allows you to accumulate sources, additional files, and generated sources for individual test scenarios.
    /// </remarks>
    protected class TestDataBuilder<TData>
        where TData : MFFGeneratorTestData, new()
    {
        private string _curName = string.Empty;
        private readonly ICollection<string> _curSources = new HashSet<string>();
        private readonly ICollection<(string, string)> _curAdditionalFiles =
            new HashSet<(string, string)>();
        private readonly ICollection<(Type, string, string)> _curGeneratedSources =
            new HashSet<(Type, string, string)>();

        private readonly ICollection<string> _allSources = new HashSet<string>();
        private readonly ICollection<(string, string)> _allAdditionalFiles =
            new HashSet<(string, string)>();
        private readonly ICollection<(Type, string, string)> _allGeneratedSources =
            new HashSet<(Type, string, string)>();

        public void BeginTest(string testName)
        {
            _curName = testName;
            _curSources.Clear();
            _curAdditionalFiles.Clear();
            _curGeneratedSources.Clear();
        }

        public void AddSource(string source)
        {
            _curSources.Add(source);
            _allSources.Add(source);
        }

        public void AddAdditionalFile((string, string) additionalFile)
        {
            _curAdditionalFiles.Add(additionalFile);
            _allAdditionalFiles.Add(additionalFile);
        }

        public void AddGeneratedSource((Type, string, string) generatedSource)
        {
            _curGeneratedSources.Add(generatedSource);
            _allGeneratedSources.Add(generatedSource);
        }

        public object[] BuildTestData()
        {
            var ret = new object[] { new TData {
                Scenario = _curName.ToString(),
                Sources = _curSources.ToArray(),
                AdditionalFiles = _curAdditionalFiles.ToArray(),
                GeneratedSources = _curGeneratedSources.ToArray()
            }};

            return ret;
        }

        public object[] BuildAllTestData()
        {
            var ret = new object[] { new TData {
                Scenario = "All_Scenarios_Together",
                Sources = _allSources.ToArray(),
                AdditionalFiles = _allAdditionalFiles.ToArray(),
                GeneratedSources = _allGeneratedSources.ToArray()
            }};

            return ret;
        }
    }

    #endregion

    #region Private/Protected properties

    protected readonly List<object[]> _data;

    #endregion

    #region Constructors

    public MFFGeneratorTestDataGeneratorBase()
    {
        _data = new List<object[]>();
    }

    #endregion

    #region IEnumerable Implementation

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion
}

