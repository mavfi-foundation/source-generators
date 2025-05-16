// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// This in a mutable version of <see cref="MFFGeneratorInfoRecord"/> and is used to store generator
/// configuration information.
/// </summary>
public class MFFGeneratorInfoModel
{
    /// <inheritdoc cref="MFFGeneratorInfoRecord.ContainingNamespace"/>
    public string? ContainingNamespace { get; set; }

    /// <inheritdoc cref="MFFGeneratorInfoRecord.SrcLocatorType"/>
    public string? SrcLocatorType { get; set; }

    /// <inheritdoc cref="MFFGeneratorInfoRecord.SrcLocatorInfo"/>
    public object? SrcLocatorInfo { get; set; }

    /// <inheritdoc cref="MFFGeneratorInfoRecord.GenOutputInfos"/>
    public List<MFFBuilderModel>? GenOutputInfos { get; set; }

    /// <inheritdoc cref="MFFGeneratorInfoRecord.SrcOutputInfos"/>
    public List<MFFBuilderModel>? SrcOutputInfos { get; set; }

}
