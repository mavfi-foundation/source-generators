// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// Enumeration for common accessibility combinations.
/// </summary>
/// <remarks>
/// This class is similar to <see href="https://learn.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.accessibility">Microsoft.CodeAnalysis.Accessibility</see> but does not require a 
/// reference to Microsoft.CodeAnalysis.
/// </remarks>
public enum MFFAccessibilityType
{
    /// <summary>
    /// No accessibility specified.
    /// </summary>
    NotApplicable = 0,

    /// <summary>
    /// Specifies that the accessibility level is private, restricting access to the containing type only.
    /// </summary>
    Private = 1,

    /// <summary>
    /// Specifies that the member is accessible only within its own class and derived classes that are in the same assembly.
    /// </summary>
    ProtectedAndInternal = 2,

    /// <summary>
    /// Specifies that the member has protected accessibility, meaning it is accessible within its own class and by derived class instances.
    /// </summary>
    Protected = 3,

    /// <summary>
    /// Specifies that the member has internal accessibility, meaning it is accessible only within its own assembly.
    /// </summary>
    Internal = 4,

    /// <summary>
    /// Specifies that the member is accessible either within its own assembly or from derived types.
    /// </summary>
    ProtectedOrInternal = 5,

    /// <summary>
    /// Specifies that the member has public accessibility.
    /// </summary>
    Public = 6
}
