// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// Represents a record for a field member within a type, inheriting from <see cref="MFFTypeMemberRecord"/>.
/// </summary>
/// <remarks>
/// This record encapsulates metadata about a field, including its name, type, accessibility, and associated attributes.
/// </remarks>
public record MFFTypeFieldRecord : MFFTypeMemberRecord
{
    /// <param name="typeMemberRecord">
    /// An instance of <see cref="MFFTypeMemberRecord"/> containing the base member information to initialize the field record.
    /// </param>
    public MFFTypeFieldRecord(
        MFFTypeMemberRecord typeMemberRecord
        )
        : this(
            typeMemberRecord.Name,
            typeMemberRecord.TypeFullyQualifiedName,
            typeMemberRecord.IsInherited,
            typeMemberRecord.IsValueType,
            typeMemberRecord.IsNullable,
            typeMemberRecord.IsGenericCollection,
            typeMemberRecord.DeclaredAccessibilty,
            typeMemberRecord.Attributes
        )
    { }

    /// <param name="name">
    /// The name of the field.
    /// </param>
    /// <param name="typeFullyQualifiedName">
    /// The fully qualified name of the field's type.
    /// </param>
    /// <param name="isInherited">
    /// Indicates whether the field is inherited from a base type.
    /// </param>
    /// <param name="isValueType">
    /// Indicates whether the field is a value type.
    /// </param>
    /// <param name="isNullable">
    /// Indicates whether the field type is nullable.
    /// </param>
    /// <param name="isGenericCollection">
    /// Indicates whether the field is a generic collection.
    /// </param>
    /// <param name="declaredAccessibilty">
    /// The declared accessibility of the field.
    /// </param>
    /// <param name="attributes">
    /// The attributes applied to the field.
    /// </param>
    public MFFTypeFieldRecord(
        string name,
        string typeFullyQualifiedName,
        bool isInherited,
        bool isValueType,
        bool isNullable,
        bool isGenericCollection,
        MFFAccessibilityType declaredAccessibilty,
        EquatableArray<MFFAttributeDataRecord> attributes)
        : base(
            name,
            typeFullyQualifiedName,
            isInherited,
            isValueType,
            isNullable,
            isGenericCollection,
            declaredAccessibilty,
            attributes
        )
    {
    }
}