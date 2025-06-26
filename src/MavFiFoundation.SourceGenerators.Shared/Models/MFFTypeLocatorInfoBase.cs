// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// Represents the base information required for locating types within assemblies.
/// </summary>
public class MFFTypeLocatorInfoBase
{
    /// <summary>
    /// Gets or sets the array of assembly names to search for types.
    /// </summary>
    public string[] Assemblies2Search { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Gets or sets the array of type names to exclude from the search.
    /// </summary>
    public string[] Types2Exclude {get; set;} = Array.Empty<string>();

    /// <summary>
    /// Gets or sets a value indicating whether to exclude types from the current project during the search.
    /// </summary>
    public bool NoSearchProjectTypes {get; set;}
}
