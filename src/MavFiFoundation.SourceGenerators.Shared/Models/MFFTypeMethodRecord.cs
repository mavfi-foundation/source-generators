// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

public record MFFTypeMethodRecord : MFFTypeMemberRecord
{
    public EquatableArray<MFFParameterRecord> Parameters { get; private set; }

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