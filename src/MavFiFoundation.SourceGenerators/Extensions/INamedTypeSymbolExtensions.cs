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

/// <summary>
/// Provides extension methods for <see cref="INamedTypeSymbol"/> and related Roslyn symbol types,
/// enabling extraction and transformation of type, property, field, method, parameter, and attribute metadata
/// into custom record types for source generation scenarios.
/// </summary>
public static class INamedTypeSymbolExtensions
{
    private const string BackingFieldSuffix = "k__BackingField";
    
    
    /// <summary>
    /// Returns the generic parameter list for the given type symbol, formatted as &lt;T1, T2, ...&gt; if any exist, or an empty string otherwise.
    /// </summary>
    /// <param name="self">The type symbol.</param>
    /// <returns>A string representing the generic parameters.</returns>
    public static string GetGenericParameters(this INamedTypeSymbol self) => self.TypeParameters.Length > 0 ?
              $"<{string.Join(", ", self.TypeParameters.Select(t => t.Name))}>" :
              string.Empty;

    /// <summary>
    /// Maps a Roslyn <see cref="Accessibility"/> value to a <see cref="MFFAccessibilityType"/>.
    /// </summary>
    /// <param name="accessibility">The accessibility value.</param>
    /// <returns>The corresponding <see cref="MFFAccessibilityType"/>.</returns>
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

    /// <summary>
    /// Creates a <see cref="MFFTypeSymbolRecord"/> representing the given type symbol, including its properties, fields, methods, and attributes.
    /// </summary>
    /// <param name="self">The type symbol.</param>
    /// <returns>A <see cref="MFFTypeSymbolRecord"/> with extracted metadata.</returns>
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

    /// <summary>
    /// Creates a <see cref="MFFTypePropertyRecord"/> for the specified property symbol, including type, accessibility, and attributes.
    /// </summary>
    /// <param name="property">The property symbol.</param>
    /// <param name="targetType">The declaring type symbol.</param>
    /// <param name="self">The root type symbol for context.</param>
    /// <returns>A <see cref="MFFTypePropertyRecord"/> with property metadata.</returns>
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

    /// <summary>
    /// Collects all property records for the given type symbol and its base types.
    /// </summary>
    /// <param name="self">The type symbol.</param>
    /// <returns>An <see cref="EquatableArray{MFFTypePropertyRecord}"/> of property records.</returns>
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

    /// <summary>
    /// Creates a <see cref="MFFTypeFieldRecord"/> for the specified field symbol, including type, accessibility, and attributes.
    /// </summary>
    /// <param name="field">The field symbol.</param>
    /// <param name="targetType">The declaring type symbol.</param>
    /// <param name="self">The root type symbol for context.</param>
    /// <returns>A <see cref="MFFTypeFieldRecord"/> with field metadata.</returns>
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

    /// <summary>
    /// Collects all field records for the given type symbol and its base types, excluding compiler-generated backing fields.
    /// </summary>
    /// <param name="self">The type symbol.</param>
    /// <returns>An <see cref="EquatableArray{MFFTypeFieldRecord}"/> of field records.</returns>
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

    /// <summary>
    /// Creates a <see cref="MFFTypeMethodRecord"/> for the specified method symbol, including return type, parameters, accessibility, and attributes.
    /// </summary>
    /// <param name="method">The method symbol.</param>
    /// <param name="targetType">The declaring type symbol.</param>
    /// <param name="self">The root type symbol for context.</param>
    /// <returns>A <see cref="MFFTypeMethodRecord"/> with method metadata.</returns>
    public static MFFTypeMethodRecord GetTypeMethodRecord(
        this IMethodSymbol method,
        INamedTypeSymbol targetType,
        INamedTypeSymbol self)
    {
        return new MFFTypeMethodRecord(
            method.Name,
            method.ReturnType.GetFullyQualifiedName(),
            !SymbolEqualityComparer.Default.Equals(targetType, self),
            method.ReturnType.IsValueType,
            method.ReturnType.NullableAnnotation == NullableAnnotation.Annotated,
            method.ReturnType.Name == "ICollection" || method.ReturnType.AllInterfaces.Any(i => i.Name == "ICollection"),
            method.DeclaredAccessibility.GetMFFAccessibility(),
            method.GetMethodParametersRecords(targetType, self),
            method.GetAttributesRecord()
        );
    }

    /// <summary>
    /// Collects all method records for the given type symbol and its base types, excluding property accessors.
    /// </summary>
    /// <param name="self">The type symbol.</param>
    /// <returns>An <see cref="EquatableArray{MFFTypeMethodRecord}"/> of method records.</returns>
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

    /// <summary>
    /// Creates a <see cref="MFFParameterRecord"/> for the specified parameter symbol, including type, accessibility, and attributes.
    /// </summary>
    /// <param name="parameter">The parameter symbol.</param>
    /// <param name="targetType">The declaring type symbol.</param>
    /// <param name="self">The root type symbol for context.</param>
    /// <returns>A <see cref="MFFParameterRecord"/> with parameter metadata.</returns>
    public static MFFParameterRecord GetMethodRecord(
        this IParameterSymbol parameter,
        INamedTypeSymbol targetType,
        INamedTypeSymbol self)
    {
        return new MFFParameterRecord(
            parameter.Name,
            parameter.Type.GetFullyQualifiedName(),
            !SymbolEqualityComparer.Default.Equals(targetType, self),
            parameter.Type.IsValueType,
            parameter.Type.NullableAnnotation == NullableAnnotation.Annotated,
            parameter.Type.Name == "ICollection" || parameter.Type.AllInterfaces.Any(i => i.Name == "ICollection"),
            parameter.DeclaredAccessibility.GetMFFAccessibility(),
            parameter.GetAttributesRecord()
        );
    }

    /// <summary>
    /// Collects all parameter records for the given method symbol.
    /// </summary>
    /// <param name="method">The method symbol.</param>
    /// <param name="targetType">The declaring type symbol.</param>
    /// <param name="self">The root type symbol for context.</param>
    /// <returns>An <see cref="EquatableArray{MFFParameterRecord}"/> of parameter records.</returns>
    public static EquatableArray<MFFParameterRecord> GetMethodParametersRecords(
        this IMethodSymbol method,
        INamedTypeSymbol targetType,
        INamedTypeSymbol self)
    {
        var methodsBuilder = ImmutableArray.CreateBuilder<MFFParameterRecord>();

        methodsBuilder.AddRange(method.Parameters
            .Select(p => p.GetMethodRecord(targetType, self)));

        return methodsBuilder.ToImmutable();
    }

    /// <summary>
    /// Gets all non-indexer property symbols for the specified type symbol.
    /// </summary>
    /// <param name="targetType">The type symbol.</param>
    /// <returns>An enumerable of <see cref="IPropertySymbol"/>.</returns>
    private static IEnumerable<IPropertySymbol> GetTypeProperties(INamedTypeSymbol targetType)
    {
        return targetType.GetMembers().OfType<IPropertySymbol>()
                        .Where(p => !p.IsIndexer);
    }

    /// <summary>
    /// Gets all field symbols for the specified type symbol, excluding compiler-generated backing fields.
    /// </summary>
    /// <param name="targetType">The type symbol.</param>
    /// <returns>An enumerable of <see cref="IFieldSymbol"/>.</returns>
    private static IEnumerable<IFieldSymbol> GetTypeFields(INamedTypeSymbol targetType)
    {
        return targetType.GetMembers().OfType<IFieldSymbol>()
            .Where(f => !f.Name.EndsWith(BackingFieldSuffix));
    }

    /// <summary>
    /// Gets all method symbols for the specified type symbol, excluding property accessors.
    /// </summary>
    /// <param name="targetType">The type symbol.</param>
    /// <returns>An enumerable of <see cref="IMethodSymbol"/>.</returns>
    private static IEnumerable<IMethodSymbol> GetTypeMethods(INamedTypeSymbol targetType)
    {
        return targetType.GetMembers().OfType<IMethodSymbol>()
            .Where(m => !m.Name.StartsWith("get_") && !m.Name.StartsWith("set_")); //TODO: Constants
    }

    /// <summary>
    /// Collects all attribute data records for the specified symbol.
    /// </summary>
    /// <param name="targetSymbol">The symbol to inspect.</param>
    /// <returns>An <see cref="EquatableArray{MFFAttributeDataRecord}"/> of attribute records.</returns>
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

    /// <summary>
    /// Collects all attribute property records for the specified attribute data, including constructor and named arguments.
    /// </summary>
    /// <param name="targetAttribute">The attribute data.</param>
    /// <returns>An <see cref="EquatableArray{MFFAttributePropertyRecord}"/> of attribute property records.</returns>
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

    /// <summary>
    /// Gets the value of a <see cref="TypedConstant"/>, handling arrays and single values.
    /// </summary>
    /// <param name="typedConstant">The typed constant.</param>
    /// <returns>The value as an object, or an array of objects if the constant is an array.</returns>
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

    /// <summary>
    /// Returns the generic type parameter constraints for the given type symbol, formatted as C# 'where' clauses.
    /// </summary>
    /// <param name="self">The type symbol.</param>
    /// <returns>A string containing the constraints, or an empty string if none.</returns>
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

    /// <summary>
    /// Returns the constraints for a single type parameter, formatted as a C# 'where' clause.
    /// </summary>
    /// <param name="self">The type parameter symbol.</param>
    /// <returns>A string containing the constraints, or an empty string if none.</returns>
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