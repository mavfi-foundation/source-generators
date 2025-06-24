// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

/**********************************************************************************
*
* Original code based on AutoDeconstruct generator created by Jason Bock
* and published in 'Writing Code to Generate Code in C#' article located at
* https://www.codemag.com/Article/2305061/Writing-Code-to-Generate-Code-in-C#
* AutoDestruct code was retrieved from https://github.com/JasonBock/AutoDeconstruct
*
***********************************************************************************/

namespace MavFiFoundation.SourceGenerators.Models;

public record MFFTypePropertyRecord : MFFTypeMemberRecord
{
    public string TypeFullyQualifiedName { get; private set; }

    public bool IsValueType { get; private set; }

    public bool IsNullable { get; private set; }

    public bool IsGenericCollection { get; private set; }

    public MFFTypeMethodRecord? GetMethod { get; private set; }

    public MFFTypeMethodRecord? SetMethod { get; private set; }


    public MFFTypePropertyRecord(
        string typeFullyQualifiedName,
        bool isValueType,
        bool isNullable,
        bool isGenericCollection,
        MFFTypeMemberRecord typeMemberRecord,
        MFFTypeMethodRecord? getMethod = null,
        MFFTypeMethodRecord? setMethod = null
        )
        : this(
            typeMemberRecord.Name,
            typeFullyQualifiedName,
            typeMemberRecord.IsInherited,
            isValueType,
            isNullable,
            isGenericCollection,
            typeMemberRecord.DeclaredAccessibilty,
            typeMemberRecord.Attributes,
            getMethod,
            setMethod
        )
    { }

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
            isInherited,
            declaredAccessibilty,
            attributes
        )
    {
        TypeFullyQualifiedName = typeFullyQualifiedName;
        IsValueType = isValueType;
        IsNullable = isNullable;
        IsGenericCollection = isGenericCollection;
        GetMethod = getMethod;
        SetMethod = setMethod;
    }
}