// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// Enumeration for common accessibility combinations.
/// </summary>
/// This class is similar to <see cref="Microsoft.CodeAnalysis.Accessibility"/> but does not require a 
/// reference to Microsoft.CodeAnalysis. 
public enum MFFAccessibilityType
{
    /// <inheritdoc cref="Microsoft.CodeAnalysis.Accessibility.NotApplicable"/>
    NotApplicable = 0,

    /// <inheritdoc cref="Microsoft.CodeAnalysis.Accessibility.Private"/>
    Private = 1,

    /// <inheritdoc cref="Microsoft.CodeAnalysis.Accessibility.ProtectedAndInternal"/>
    ProtectedAndInternal = 2,

    /// <inheritdoc cref="Microsoft.CodeAnalysis.Accessibility.Protected"/>
    Protected = 3,

    /// <inheritdoc cref="Microsoft.CodeAnalysis.Accessibility.Internal"/>
    Internal = 4,

    /// <inheritdoc cref="Microsoft.CodeAnalysis.Accessibility.ProtectedOrInternal"/>
    ProtectedOrInternal = 5,

    /// <inheritdoc cref="Microsoft.CodeAnalysis.Accessibility.Public"/>
    Public = 6
}
