// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// Used to store builder configuration information.
/// </summary>
/// <param name="FileNameBuilderType">The name of the builder to use to build the file name for the 
/// generated source.</param>
/// <param name="FileNameBuilderInfo">Configuration information for the specified builder used to 
/// build the file name for the generated source.</param>
/// <param name="SourceBuilderType">The name of the builder to use to build the generated source.</param>
/// <param name="SourceBuilderInfo">Configuration information for the specified builder used to 
/// build the generated source.</param>
/// <param name="AdditionalOutputInfos">Additional information to be passed to the specified builder.</param>
public record MFFBuilderRecord(

    string? FileNameBuilderType,
    string FileNameBuilderInfo,
    string SourceBuilderType,
    object SourceBuilderInfo,
    EquatableArray<(string Key, object Value)> AdditionalOutputInfos

//TODO: Diff Path for templates support - Maybe added to AdditionalOutputInfos - need to consider loading from resources
);
