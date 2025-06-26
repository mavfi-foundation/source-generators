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

/// <summary>
/// Represents a record containing metadata information about a type symbol, including its namespace, name, generic parameters, 
/// fully qualified name, constraints, value type status, and collections of its properties, fields, methods, and attributes.
/// </summary>
/// <param name="ContainingNamespace">The namespace that contains the type.</param>
/// <param name="Name">The simple name of the type.</param>
/// <param name="GenericParameters">The generic parameters of the type, if any.</param>
/// <param name="FullyQualifiedName">The fully qualified name of the type.</param>
/// <param name="Constraints">The generic constraints applied to the type, if any.</param>
/// <param name="IsValueType">Indicates whether the type is a value type.</param>
/// <param name="Properties">A collection of property records associated with the type.</param>
/// <param name="Fields">A collection of field records associated with the type.</param>
/// <param name="Methods">A collection of method records associated with the type.</param>
/// <param name="Attributes">A collection of attribute records applied to the type.</param>
public record MFFTypeSymbolRecord
(
    string ContainingNamespace,
    string Name,
    string GenericParameters,
    string FullyQualifiedName,
    string Constraints,
    bool IsValueType,
    EquatableArray<MFFTypePropertyRecord> Properties,
    EquatableArray<MFFTypeFieldRecord> Fields,
    EquatableArray<MFFTypeMethodRecord> Methods,
    EquatableArray<MFFAttributeDataRecord> Attributes
);