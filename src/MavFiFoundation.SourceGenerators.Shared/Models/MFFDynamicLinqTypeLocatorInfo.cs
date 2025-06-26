// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// Represents locator information for dynamic LINQ queries, extending <see cref="MFFTypeLocatorInfoBase"/>.
/// </summary>
/// <remarks>
/// This class is used to encapsulate LINQ 'where' clause information for dynamic query generation.
/// </remarks>
public class MFFDynamicLinqTypeLocatorInfo : MFFTypeLocatorInfoBase
{
    /// <summary>
    /// Gets or sets the LINQ 'where' clause used for dynamic filtering.
    /// </summary>
    /// <value>
    /// A string representing the LINQ 'where' condition. Defaults to an empty string.
    /// </value>
    public string LinqWhere { get; set; } = string.Empty;
}
