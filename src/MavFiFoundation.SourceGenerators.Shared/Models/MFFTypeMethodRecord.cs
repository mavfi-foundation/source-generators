// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// Represents a method member within a type, including its parameters and associated metadata.
/// Inherits from <see cref="MFFTypeMemberRecord"/>.
/// </summary>
/// <remarks>
/// This record encapsulates metadata about a field, including its name, type, accessibility, and associated attributes.
/// </remarks>
public record MFFTypeMethodRecord : MFFTypeMemberRecord
{
    /// <summary>
    /// Gets the parameters of the method.
    /// </summary>
    public EquatableArray<MFFParameterRecord> Parameters { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MFFTypeMethodRecord"/> class
    /// using an existing <see cref="MFFTypeMemberRecord"/> and a set of parameters.
    /// </summary>
    /// <param name="parameters">The parameters of the method.</param>
    /// <param name="typeMemberRecord">The base type member record containing metadata.</param>
    public MFFTypeMethodRecord(
        EquatableArray<MFFParameterRecord> parameters,
        MFFTypeMemberRecord typeMemberRecord
        )
        : this(
            typeMemberRecord.Name,
            typeMemberRecord.TypeFullyQualifiedName,
            typeMemberRecord.IsValueType,
            typeMemberRecord.IsNullable,
            typeMemberRecord.IsGenericCollection,
            typeMemberRecord.IsInherited,
            typeMemberRecord.DeclaredAccessibilty,
            parameters,
            typeMemberRecord.Attributes
        )
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MFFTypeMethodRecord"/> class
    /// with the specified method metadata and parameters.
    /// </summary>
    /// <param name="name">The name of the method.</param>
    /// <param name="typeFullyQualifiedName">The fully qualified type name of the method's return type.</param>
    /// <param name="isInherited">Indicates whether the method is inherited.</param>
    /// <param name="isValueType">Indicates whether the method's return type is a value type.</param>
    /// <param name="isNullable">Indicates whether the method's return type is nullable.</param>
    /// <param name="isGenericCollection">Indicates whether the method's return type is a generic collection.</param>
    /// <param name="declaredAccessibilty">The declared accessibility of the method.</param>
    /// <param name="parameters">The parameters of the method.</param>
    /// <param name="attributes">The attributes applied to the method.</param>
    public MFFTypeMethodRecord(
        string name,
        string typeFullyQualifiedName,
        bool isInherited,
        bool isValueType,
        bool isNullable,
        bool isGenericCollection,
        MFFAccessibilityType declaredAccessibilty,
        EquatableArray<MFFParameterRecord> parameters,
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
        Parameters = parameters;
    }
}