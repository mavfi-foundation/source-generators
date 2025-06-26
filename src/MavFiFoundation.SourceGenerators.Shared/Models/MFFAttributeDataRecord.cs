// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// Represents the data of a custom attribute, including its name and associated properties.
/// </summary>
/// <param name="Name">The name of the attribute.</param>
/// <param name="Properties">A collection of attribute property records associated with the attribute.</param>
public record MFFAttributeDataRecord
(
    string Name,

    EquatableArray<MFFAttributePropertyRecord> Properties

);
