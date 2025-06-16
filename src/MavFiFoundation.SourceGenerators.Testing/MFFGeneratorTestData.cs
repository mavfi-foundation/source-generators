// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Testing;

/// <summary>
/// Represents test data for source generator testing scenarios.
/// </summary>
public class MFFGeneratorTestData : MFFTestDataBase
{
    /// <summary>
    /// Gets or sets the collection of expected generated sources as tuples containing the generator type,
    /// file name, and generated content.
    /// </summary>
    public IEnumerable<(Type, string, string)> GeneratedSources { get; set; } =
        new HashSet<(Type, string, string)>();
}
