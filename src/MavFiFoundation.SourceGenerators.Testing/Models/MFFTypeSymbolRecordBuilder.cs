// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Testing.Models;

public class MFFTypeSymbolRecordBuilder
{
    #region Constants

    public const string DefaultContainingNamespace = "TestSpace";
    public const string DefaultName = "Name";
    public const string DefaultGenericParameters = "";
    public const string DefaultFullyQualifiedName = "TestSpace.Name";
    public const string DefaultConstraints = "";
    public const bool DefaultIsValueType = false;

    #endregion

    #region Private/Protected Fields/Properties

    private string _containingNamespace = DefaultContainingNamespace;
	private string _name = DefaultName;
	private string _genericParameters = DefaultGenericParameters;
	private string _fullyQualifiedName = DefaultFullyQualifiedName;
	private string _constraints = DefaultConstraints;
	private bool _isValueType = DefaultIsValueType;
	private IEnumerable<MFFTypePropertyRecord> _accessibleProperties = Array.Empty<MFFTypePropertyRecord>();
	private IEnumerable<MFFAttributeDataRecord> _attributes = Array.Empty<MFFAttributeDataRecord>();

    #endregion

    #region Public Methods

    public MFFTypeSymbolRecordBuilder ContainingNamespace(string containingNamespace)
    {
        _containingNamespace = containingNamespace;
        return this;
    }

    public MFFTypeSymbolRecordBuilder Name(string name)
    {
        _name = name;
        return this;
    }

    public MFFTypeSymbolRecordBuilder GenericParameters(string genericParameters)
    {
        _genericParameters = genericParameters;
        return this;
    }

    public MFFTypeSymbolRecordBuilder FullyQualifiedName(string fullyQualifiedName)
    {
        _fullyQualifiedName = fullyQualifiedName;
        return this;
    }

    public MFFTypeSymbolRecordBuilder Constraints(string constraints)
    {
        _constraints = constraints;
        return this;
    }

    public MFFTypeSymbolRecordBuilder IsValueType(bool isValueType)
    {
        _isValueType = isValueType;
        return this;
    }

    public MFFTypeSymbolRecordBuilder AccessibleProperties(
        IEnumerable<MFFTypePropertyRecord> accessibleProperties)
    {
        _accessibleProperties = accessibleProperties;
        return this;
    }

    public MFFTypeSymbolRecordBuilder AddAccessiblePropertie(MFFTypePropertyRecord accessibleProperty)
    {
        _accessibleProperties = _accessibleProperties.Append(accessibleProperty);
        return this;
    }

    public MFFTypeSymbolRecordBuilder Attributes(
        IEnumerable<MFFAttributeDataRecord> attributes)
    {
        _attributes = attributes;
        return this;
    }

    public MFFTypeSymbolRecordBuilder AddAttribute(MFFAttributeDataRecord attribute)
    {
        _attributes = _attributes.Append(attribute);
        return this;
    }

    public MFFTypeSymbolRecord Build()
    {
        return new MFFTypeSymbolRecord(
            _containingNamespace, 
            _name, _genericParameters, 
            _fullyQualifiedName, 
            _constraints, 
            _isValueType, 
            _accessibleProperties.ToImmutableArray(), 
            _attributes.ToImmutableArray());
    }

    #endregion
}
