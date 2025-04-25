/**********************************************************************************
*
* Original code based on AutoDeconstruct generator created by Jason Bock
* and published in 'Writing Code to Generate Code in C#' article located at
* https://www.codemag.com/Article/2305061/Writing-Code-to-Generate-Code-in-C#
* AutoDestruct code was retrieved from https://github.com/JasonBock/AutoDeconstruct
*
***********************************************************************************/

namespace MavFiFoundation.SourceGenerators.Models;

public record MFFTypeSymbolRecord
(
    string ContainingNamespace,
	string Name,
	string GenericParameters,
	string FullyQualifiedName,
	string Constraints,
	bool IsValueType,
	EquatableArray<MFFPropertySymbolRecord> AccessibleProperties,
	EquatableArray<MFFAttributeDataRecord> Attributes
);