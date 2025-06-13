// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Testing;

public abstract class MFFGeneratorTestDataProviderBase : MFFTestDataProviderBase, IEnumerable<object[]>
{

    #region GeneratorTestDataBuilder<TData>

    /// <summary>
    /// Provides a builder for constructing test data instances of type <typeparamref name="TData"/> for use in source generator testing.
    /// </summary>
    /// <typeparam name="TData">
    /// The type of test data to build. Must inherit from <see cref="MFFGeneratorTestData"/> and have a parameterless constructor.
    /// </typeparam>
    protected class GeneratorTestDataBuilder<TData> : TestDataBuilder<TData>
        where TData : MFFGeneratorTestData, new()
    {
        private readonly ICollection<(Type, string, string)> _curGeneratedSources =
            new HashSet<(Type, string, string)>();

        private readonly ICollection<(Type, string, string)> _allGeneratedSources =
            new HashSet<(Type, string, string)>();

        public override void BeginTest(string testName)
        {
            base.BeginTest(testName);
            _curGeneratedSources.Clear();
        }

        public void AddGeneratedSource((Type, string, string) generatedSource)
        {
            _curGeneratedSources.Add(generatedSource);
            _allGeneratedSources.Add(generatedSource);
        }

        protected override void AddTestData(TData testData)
        {
            testData.GeneratedSources = _curGeneratedSources.ToArray();
        }

        protected override void AddAllTestData(TData testData)
        {
            testData.GeneratedSources = _allGeneratedSources.ToArray();
        }
    }

    #endregion
}

