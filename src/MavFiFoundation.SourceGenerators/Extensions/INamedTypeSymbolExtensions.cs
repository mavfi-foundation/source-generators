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

using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using  MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators;

public static class INamedTypeSymbolExtensions
{
    private const string BackingFieldSuffix = "k__BackingField";
    
    
   public static string GetGenericParameters(this INamedTypeSymbol self) => self.TypeParameters.Length > 0 ?
              $"<{string.Join(", ", self.TypeParameters.Select(t => t.Name))}>" :
              string.Empty;

    public static MFFAccessibilityType GetMFFAccessibility(
         this Accessibility accessibility)
    {
        switch (accessibility)
        {
            case Accessibility.NotApplicable:
                return MFFAccessibilityType.NotApplicable;
            case Accessibility.Private:
                return MFFAccessibilityType.Private;
            case Accessibility.ProtectedAndInternal:
                return MFFAccessibilityType.ProtectedAndInternal;
            case Accessibility.Protected:
                return MFFAccessibilityType.Protected;
            case Accessibility.Internal:
                return MFFAccessibilityType.Internal;
            case Accessibility.ProtectedOrInternal:
                return MFFAccessibilityType.ProtectedOrInternal;
            case Accessibility.Public:
                return MFFAccessibilityType.Public;
            default:
                throw new NotSupportedException($"accessibility: {accessibility}");
        }
    }

    public static MFFTypeSymbolRecord GetTypeSymbolRecord(
        this INamedTypeSymbol self)
    {
        var properties = self.GetTypePropertyRecords();
        var fields = self.GetTypeFieldRecords();
        var methods = self.GetTypeMethodRecords();

        return new MFFTypeSymbolRecord(
                        self.ContainingNamespace.ToString(),
                        self.Name,
                        self.GetGenericParameters(),
                        self.GetFullyQualifiedName(),
                        self.GetConstraints(),
                        self.IsValueType,
                        properties,
                        fields,
                        methods,
                        self.GetAttributesRecord());
    }

    public static MFFTypePropertyRecord GetTypePropertyRecord(
        this IPropertySymbol property,
        INamedTypeSymbol targetType,
        INamedTypeSymbol self)
    {
        return new MFFTypePropertyRecord(
            property.Name,
            property.Type.GetFullyQualifiedName(),
            !SymbolEqualityComparer.Default.Equals(targetType, self),
            property.Type.IsValueType,
            property.Type.NullableAnnotation == NullableAnnotation.Annotated,
            property.Type.Name == "ICollection" || property.Type.AllInterfaces.Any(i => i.Name == "ICollection"),
            property.DeclaredAccessibility.GetMFFAccessibility(),
            property.GetAttributesRecord(),
            property.GetMethod?.GetTypeMethodRecord(targetType, self),
            property.SetMethod?.GetTypeMethodRecord(targetType, self)
        );
    }

    public static EquatableArray<MFFTypePropertyRecord> GetTypePropertyRecords(
        this INamedTypeSymbol self)
    {
        var targetType = self;
        var propertiesBuilder = ImmutableArray.CreateBuilder<MFFTypePropertyRecord>();

        while (targetType is not null)
        {
            propertiesBuilder.AddRange(GetTypeProperties(targetType)
                .Select(p =>p.GetTypePropertyRecord(targetType, self)));
            targetType = targetType.BaseType;
        }

        return propertiesBuilder.ToImmutable();
    }

    public static MFFTypeFieldRecord GetTypeFieldRecord(
        this IFieldSymbol field,
        INamedTypeSymbol targetType,
        INamedTypeSymbol self)
    {
        return new MFFTypeFieldRecord(
            field.Name,
            field.Type.GetFullyQualifiedName(),
            !SymbolEqualityComparer.Default.Equals(targetType, self),
            field.Type.IsValueType,
            field.Type.NullableAnnotation == NullableAnnotation.Annotated,
            field.Type.Name == "ICollection" || field.Type.AllInterfaces.Any(i => i.Name == "ICollection"),
            field.DeclaredAccessibility.GetMFFAccessibility(),
            field.GetAttributesRecord()
        );
    }

    public static EquatableArray<MFFTypeFieldRecord> GetTypeFieldRecords(
        this INamedTypeSymbol self)
    {
        var targetType = self;
        var fieldsBuilder = ImmutableArray.CreateBuilder<MFFTypeFieldRecord>();

        while (targetType is not null)
        {
            fieldsBuilder.AddRange(GetTypeFields(targetType)
                .Select(f => f.GetTypeFieldRecord(targetType, self)));
            targetType = targetType.BaseType;
        }

        return fieldsBuilder.ToImmutable();
    }

    public static MFFTypeMethodRecord GetTypeMethodRecord(
        this IMethodSymbol method,
        INamedTypeSymbol targetType,
        INamedTypeSymbol self)
    {
        //TODO Parameters
        //method.Parameters
        return new MFFTypeMethodRecord(
            method.Name,
            !SymbolEqualityComparer.Default.Equals(targetType, self),
            method.DeclaredAccessibility.GetMFFAccessibility(),
            method.GetAttributesRecord()
        );
    }

    public static EquatableArray<MFFTypeMethodRecord> GetTypeMethodRecords(
        this INamedTypeSymbol self)
    {
        var targetType = self;
        var methodsBuilder = ImmutableArray.CreateBuilder<MFFTypeMethodRecord>();

        while (targetType is not null)
        {
            methodsBuilder.AddRange(GetTypeMethods(targetType)
                .Select(m => m.GetTypeMethodRecord(targetType, self)));
            targetType = targetType.BaseType;
        }

        return methodsBuilder.ToImmutable();
    }

    private static IEnumerable<IPropertySymbol> GetTypeProperties(INamedTypeSymbol targetType)
    {
        return targetType.GetMembers().OfType<IPropertySymbol>()
                        .Where(p => !p.IsIndexer);
    }

    private static IEnumerable<IFieldSymbol> GetTypeFields(INamedTypeSymbol targetType)
    {
        return targetType.GetMembers().OfType<IFieldSymbol>()
            .Where(f => !f.Name.EndsWith(BackingFieldSuffix));
    }

    private static IEnumerable<IMethodSymbol> GetTypeMethods(INamedTypeSymbol targetType)
    {
        return targetType.GetMembers().OfType<IMethodSymbol>()
            .Where(m => !m.Name.StartsWith("get_") && !m.Name.StartsWith("set_")); //TODO: Constants
    }
    public static EquatableArray<MFFAttributeDataRecord> GetAttributesRecord(
        this ISymbol targetSymbol)
    {
        var propertiesBuilder = ImmutableArray.CreateBuilder<MFFAttributeDataRecord>();

        propertiesBuilder.AddRange(targetSymbol.GetAttributes()
            .Select(a => new MFFAttributeDataRecord(
                a.AttributeClass?.ToDisplayString() ?? string.Empty,
                a.GetAttributePropertiesRecord()
            )));

        return propertiesBuilder.ToImmutable();
    }

	public static EquatableArray<MFFAttributePropertyRecord> GetAttributePropertiesRecord(
		this AttributeData targetAttribute)
	{
		var propertiesBuilder = ImmutableArray.CreateBuilder<MFFAttributePropertyRecord>();

		if(targetAttribute.AttributeConstructor is not null)
		{
        	propertiesBuilder.AddRange(targetAttribute.AttributeConstructor.Parameters
				.Select(p => new MFFAttributePropertyRecord(
					p.Name,
					targetAttribute.ConstructorArguments[p.Ordinal].GetValue(),
					MFFAttributePropertyLocationType.Constructor
			)));
		}

        propertiesBuilder.AddRange(targetAttribute.NamedArguments
			.Select(na => new MFFAttributePropertyRecord(
				na.Key,
				na.Value.GetValue(),
				MFFAttributePropertyLocationType.NamedValue
		)));

        return propertiesBuilder.ToImmutable();
	}

	public static object? GetValue(
		this TypedConstant typedConstant)
	{
		if (typedConstant.Kind != TypedConstantKind.Array)
		{
			return typedConstant.Value;
		}
		else if (typedConstant.Values.Length > 0)
		{
			var valuesBuilder = ImmutableArray.CreateBuilder<object?>();

        	valuesBuilder.AddRange(typedConstant.Values
				.Select(p => p.GetValue()));

			return valuesBuilder.ToImmutable();
		}

		return null;
	}

    public static string GetConstraints(this INamedTypeSymbol self)
	{
		if (self.TypeParameters.Length == 0)
		{
			return string.Empty;
		}
		else
		{
			var constraints = new List<string>(self.TypeParameters.Length);

			foreach (var parameter in self.TypeParameters)
			{
				var parameterConstraints = parameter.GetConstraints();

				if (parameterConstraints.Length > 0)
				{
					constraints.Add(parameterConstraints);
				}
			}

			return constraints.Count > 0 ?
				string.Join(" ", constraints) :
				string.Empty;
		}
	}

	private static string GetConstraints(this ITypeParameterSymbol self)
	{
		var constraints = new List<string>();

		// Based on what I've read here:
		// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/classes#1425-type-parameter-constraints
		// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/where-generic-type-constraint
		// ...
		// Things like notnull and unmanaged should go first

		// According to CS0449, if any of these constraints exist: 
		// 'class', 'struct', 'unmanaged', 'notnull', and 'default'
		// they should not be duplicated.
		// Side note, I don't know how to find if the 'default'
		// constraint exists.
		if (self.HasUnmanagedTypeConstraint)
		{
			constraints.Add("unmanaged");
		}
		else if (self.HasNotNullConstraint)
		{
			constraints.Add("notnull");
		}
		// Then class constraint (HasReferenceTypeConstraint) or struct (HasValueTypeConstraint)
		else if (self.HasReferenceTypeConstraint)
		{
			constraints.Add(self.ReferenceTypeConstraintNullableAnnotation == NullableAnnotation.Annotated ? "class?" : "class");
		}
		else if (self.HasValueTypeConstraint)
		{
			constraints.Add("struct");
		}

		// Then type constraints (classes first, then interfaces, then other generic type parameters)
		constraints.AddRange(self.ConstraintTypes.Where(_ => _.TypeKind == TypeKind.Class).Select(_ => _.GetFullyQualifiedName()));
		constraints.AddRange(self.ConstraintTypes.Where(_ => _.TypeKind == TypeKind.Interface).Select(_ => _.GetFullyQualifiedName()));
		constraints.AddRange(self.ConstraintTypes.Where(_ => _.TypeKind == TypeKind.TypeParameter).Select(_ => _.GetFullyQualifiedName()));

		// Then constructor constraint
		if (self.HasConstructorConstraint)
		{
			constraints.Add("new()");
		}

		return constraints.Count == 0 ? string.Empty :
			$"where {self.Name} : {string.Join(", ", constraints)}";
	}
}