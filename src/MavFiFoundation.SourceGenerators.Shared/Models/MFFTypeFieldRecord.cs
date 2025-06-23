// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

public record MFFTypeFieldRecord : MFFTypeMemberRecord
{
    public string TypeFullyQualifiedName { get; private set; }

    public bool IsValueType { get; private set; }

    public bool IsNullable { get; private set; }

    public bool IsGenericCollection { get; private set; }


    public MFFTypeFieldRecord(
        string typeFullyQualifiedName,
        bool isValueType,
        bool isNullable,
        bool isGenericCollection,
        MFFTypeMemberRecord typeMemberRecord
        )
        : this(
            typeMemberRecord.Name,
            typeFullyQualifiedName,
            typeMemberRecord.IsInherited,
            isValueType,
            isNullable,
            isGenericCollection,
            typeMemberRecord.DeclaredAccessibilty,
            typeMemberRecord.Attributes
        )
    { }

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
            isInherited,
            declaredAccessibilty,
            attributes
        )
    {
        TypeFullyQualifiedName = typeFullyQualifiedName;
        IsValueType = isValueType;
        IsNullable = isNullable;
        IsGenericCollection = isGenericCollection;
    }
}