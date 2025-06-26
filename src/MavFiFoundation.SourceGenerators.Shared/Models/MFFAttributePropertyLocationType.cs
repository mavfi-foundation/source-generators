// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// Specifies the location type of an attribute property in the source code.
/// </summary>
public enum MFFAttributePropertyLocationType
{
    /// <summary>
    /// Indicates that the property is set via the attribute's constructor.
    /// </summary>
    Constructor,

    /// <summary>
    /// Indicates that the property is set using a named argument.
    /// </summary>
    NamedValue
}
