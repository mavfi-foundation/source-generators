// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

public record MFFTypeMemberRecord
(
    string Name,
    
    bool IsInherited,

    MFFAccessibilityType DeclaredAccessibilty,

    EquatableArray<MFFAttributeDataRecord> Attributes
);
