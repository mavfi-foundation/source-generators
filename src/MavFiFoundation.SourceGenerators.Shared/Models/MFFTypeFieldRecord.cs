// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

public record MFFTypeFieldRecord : MFFTypeMemberRecord
{
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