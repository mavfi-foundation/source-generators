// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// This is a mutable version of <see cref="MFFBuilderRecord"/> used to store builder 
/// configuration information.
/// </summary>
public class MFFBuilderModel
{
    /// <inheritdoc cref="MFFBuilderRecord.FileNameBuilderType"/>
    public string? FileNameBuilderType { get; set; }

    /// <inheritdoc cref="MFFBuilderRecord.FileNameBuilderInfo"/>
    public string? FileNameBuilderInfo { get; set; }

    /// <inheritdoc cref="MFFBuilderRecord.SourceBuilderType"/>
    public string? SourceBuilderType { get; set; }

    /// <inheritdoc cref="MFFBuilderRecord.SourceBuilderInfo"/>
    public object? SourceBuilderInfo { get; set; }

    /// <inheritdoc cref="MFFBuilderRecord.AdditionalOutputInfos"/>
    public Dictionary<string, object>? AdditionalOutputInfos { get; set; }
}
