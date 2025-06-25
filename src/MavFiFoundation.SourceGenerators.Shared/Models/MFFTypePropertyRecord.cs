// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

public record MFFTypePropertyRecord : MFFTypeMemberRecord
{
    public MFFTypeMethodRecord? GetMethod { get; private set; }

    public MFFTypeMethodRecord? SetMethod { get; private set; }


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