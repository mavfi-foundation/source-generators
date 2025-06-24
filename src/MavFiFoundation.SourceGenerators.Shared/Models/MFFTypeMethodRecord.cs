// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

public record MFFTypeMethodRecord : MFFTypeMemberRecord
{

    public MFFTypeMethodRecord(
        MFFTypeMemberRecord typeMemberRecord
        )
        : this(
            typeMemberRecord.Name,
            typeMemberRecord.IsInherited,
            typeMemberRecord.DeclaredAccessibilty,
            typeMemberRecord.Attributes
        )
    { }

    public MFFTypeMethodRecord(
        string name,
        bool isInherited,
        MFFAccessibilityType declaredAccessibilty,
        EquatableArray<MFFAttributeDataRecord> attributes)
        : base(
            name,
            isInherited,
            declaredAccessibilty,
            attributes
        )
    {
    }
}