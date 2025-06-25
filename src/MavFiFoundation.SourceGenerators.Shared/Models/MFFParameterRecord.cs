// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// Represents a parameter record that extends <see cref="MFFTypeMemberRecord"/> with additional parameter-specific information.
/// </summary>
/// <remarks>
/// This record is used to encapsulate metadata about a parameter, including its name, type, inheritance status, value type status,
/// nullability, generic collection status, declared accessibility, and associated attributes.
/// </remarks>
public record MFFParameterRecord : MFFTypeMemberRecord
{
    /// <param name="typeMemberRecord">
    /// An instance of <see cref="MFFTypeMemberRecord"/> whose properties are used to initialize the parameter record.
    /// </param>
    public MFFParameterRecord(
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

    /// <param name="name">The name of the parameter.</param>
    /// <param name="typeFullyQualifiedName">The fully qualified type name of the parameter.</param>
    /// <param name="isInherited">Indicates whether the parameter is inherited.</param>
    /// <param name="isValueType">Indicates whether the parameter is a value type.</param>
    /// <param name="isNullable">Indicates whether the parameter is nullable.</param>
    /// <param name="isGenericCollection">Indicates whether the parameter is a generic collection.</param>
    /// <param name="declaredAccessibilty">The declared accessibility of the parameter.</param>
    /// <param name="attributes">A collection of attributes applied to the parameter.</param>
    public MFFParameterRecord(
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
