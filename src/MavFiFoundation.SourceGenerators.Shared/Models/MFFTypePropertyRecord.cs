// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// Represents a property member within a type, including its getter and setter methods, 
/// as well as metadata inherited from <see cref="MFFTypeMemberRecord"/>.
/// </summary>
/// <remarks>
/// This record encapsulates information about a property, such as its name, type, accessibility,
/// attributes, and associated get/set methods.
/// </remarks>
public record MFFTypePropertyRecord : MFFTypeMemberRecord
{
    /// <summary>
    /// Gets the getter method of the property, if available.
    /// </summary>
    public MFFTypeMethodRecord? GetMethod { get; private set; }

    /// <summary>
    /// Gets the setter method of the property, if available.
    /// </summary>
    public MFFTypeMethodRecord? SetMethod { get; private set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="MFFTypePropertyRecord"/> class
    /// using an existing <see cref="MFFTypeMemberRecord"/> and a set of parameters.
    /// </summary>
    /// <param name="typeMemberRecord">The base type member record containing metadata.</param>
    /// <param name="getMethod">
    /// The getter method record for the property, if available.
    /// </param>
    /// <param name="setMethod">
    /// The setter method record for the property, if available.
    /// </param>
    public MFFTypePropertyRecord(
        MFFTypeMemberRecord typeMemberRecord,
        MFFTypeMethodRecord? getMethod = null,
        MFFTypeMethodRecord? setMethod = null
        )
        : this(
            typeMemberRecord.Name,
            typeMemberRecord.TypeFullyQualifiedName,
            typeMemberRecord.IsInherited,
            typeMemberRecord.IsValueType,
            typeMemberRecord.IsNullable,
            typeMemberRecord.IsGenericCollection,
            typeMemberRecord.DeclaredAccessibilty,
            typeMemberRecord.Attributes,
            getMethod,
            setMethod
        )
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MFFTypePropertyRecord"/> class
    /// with the specified property metadata and parameters.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="typeFullyQualifiedName">The fully qualified type name of the property's type.</param>
    /// <param name="isInherited">Indicates whether the property is inherited.</param>
    /// <param name="isValueType">Indicates whether the property's type is a value type.</param>
    /// <param name="isNullable">Indicates whether the property's type is nullable.</param>
    /// <param name="isGenericCollection">Indicates whether the property's type is a generic collection.</param>
    /// <param name="declaredAccessibilty">The declared accessibility of the property.</param>
    /// <param name="parameters">The parameters of the property.</param>
    /// <param name="attributes">The attributes applied to the property.</param>
    /// <inheritdoc cref="MFFTypePropertyRecord.MFFTypePropertyRecord(MFFTypeMemberRecord, MFFTypeMethodRecord?, MFFTypeMethodRecord?)" 
    ///     path="/param[@name='getMethod']"/>
    /// <inheritdoc cref="MFFTypePropertyRecord.MFFTypePropertyRecord(MFFTypeMemberRecord, MFFTypeMethodRecord?, MFFTypeMethodRecord?)" 
    ///     path="/param[@name='setMethod']"/>
    public MFFTypePropertyRecord(
        string name,
        string typeFullyQualifiedName,
        bool isInherited,
        bool isValueType,
        bool isNullable,
        bool isGenericCollection,
        MFFAccessibilityType declaredAccessibilty,
        EquatableArray<MFFAttributeDataRecord> attributes,
        MFFTypeMethodRecord? getMethod = null,
        MFFTypeMethodRecord? setMethod = null)
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
        GetMethod = getMethod;
        SetMethod = setMethod;
    }
}