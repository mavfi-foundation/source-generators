/**********************************************************************************
*
* Original code based on AutoDeconstruct generator created by Jason Bock
* and published in 'Writing Code to Generate Code in C#' article located at
* https://www.codemag.com/Article/2305061/Writing-Code-to-Generate-Code-in-C#
* AutoDestruct code was retrieved from https://github.com/JasonBock/AutoDeconstruct
*
***********************************************************************************/

using System.Collections.Immutable;

namespace MavFiFoundation.SourceGenerators.Models;

public record MFFPropertySymbolRecord
(
    string Name,
    
    string TypeFullyQualifiedName,

    bool IsInherited,

    bool IsValueType,

    bool IsNullable,

    bool IsGenericCollection,

	EquatableArray<MFFAttributeDataRecord> Attributes
);
