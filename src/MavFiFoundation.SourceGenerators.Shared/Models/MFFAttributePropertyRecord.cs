// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// Represents a record for an attribute property, including its name, value, and source location type.
/// </summary>
/// <param name="Name">The name of the attribute property.</param>
/// <param name="Value">The value assigned to the attribute property. Can be <c>null</c>.</param>
/// <param name="From">The location type indicating where the property was sourced from.</param>
public record class MFFAttributePropertyRecord(
    string Name,
    object? Value,
    MFFAttributePropertyLocationType From);