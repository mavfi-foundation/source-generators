// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// Represents locator information for a specific attribute type within the MavFi Foundation source generators.
/// Inherits from <see cref="MFFTypeLocatorInfoBase"/>.
/// </summary>
/// <remarks>
/// This class is used to specify the name of the attribute to locate during source generation.
/// </remarks>
public class MFFAttributeTypeLocatorInfo : MFFTypeLocatorInfoBase
{
    /// <summary>
    /// Gets or sets the name of the attribute to find.
    /// </summary>
    public string Attribute2Find { get; set; } = string.Empty;
}
