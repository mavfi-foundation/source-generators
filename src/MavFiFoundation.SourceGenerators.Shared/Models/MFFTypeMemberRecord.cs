// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// Represents a member of a type within the MavFi Foundation source generators,
/// encapsulating metadata such as name, type, accessibility, and attributes.
/// </summary>
/// <param name="Name">The name of the member.</param>
/// <param name="TypeFullyQualifiedName">The fully qualified name of the member's type.</param>
/// <param name="IsInherited">Indicates whether the member is inherited from a base type.</param>
/// <param name="IsValueType">Indicates whether the member's type is a value type.</param>
/// <param name="IsNullable">Indicates whether the member's type is nullable.</param>
/// <param name="IsGenericCollection">Indicates whether the member's type is a generic collection.</param>
/// <param name="DeclaredAccessibilty">The declared accessibility of the member.</param>
/// <param name="Attributes">The collection of attributes applied to the member.</param>
public record MFFTypeMemberRecord
(
    string Name,
    string TypeFullyQualifiedName,
    bool IsInherited,
    bool IsValueType,
    bool IsNullable,
    bool IsGenericCollection,
    MFFAccessibilityType DeclaredAccessibilty,
    EquatableArray<MFFAttributeDataRecord> Attributes
);
