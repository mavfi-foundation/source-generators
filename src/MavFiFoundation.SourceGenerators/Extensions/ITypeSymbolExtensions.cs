/**********************************************************************************
*
* Original code based on AutoDeconstruct generator created by Jason Bock
* and published in 'Writing Code to Generate Code in C#' article located at
* https://www.codemag.com/Article/2305061/Writing-Code-to-Generate-Code-in-C#
* AutoDestruct code was retrieved from https://github.com/JasonBock/AutoDeconstruct
*
***********************************************************************************/ 

using Microsoft.CodeAnalysis;

namespace MavFiFoundation.SourceGenerators;

internal static class ITypeSymbolExtensions
{
	internal static string GetFullyQualifiedName(this ITypeSymbol self)
	{
		var symbolFormatter = SymbolDisplayFormat.FullyQualifiedFormat.
			WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.Omitted).
			AddMiscellaneousOptions(SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier);
		return self.ToDisplayString(symbolFormatter);
	}
}