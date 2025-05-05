using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Testing.Models;

public class MFFTypeSymbolRecordBuilder
{
    private string _containingNamespace = "TestSpace";
	private string _name = "Name";
	private string _genericParameters = string.Empty;
	private string _fullyQualifiedName = "TestSpace.Name";
	private string _constraints = string.Empty;
	private bool _isValueType = false;
	private IEnumerable<MFFPropertySymbolRecord> _accessibleProperties = Array.Empty<MFFPropertySymbolRecord>();
	private IEnumerable<MFFAttributeDataRecord> _attributes = Array.Empty<MFFAttributeDataRecord>();

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
        IEnumerable<MFFPropertySymbolRecord> accessibleProperties)
    {
        _accessibleProperties = accessibleProperties;
        return this;
    }

    public MFFTypeSymbolRecordBuilder AddAccessiblePropertie(MFFPropertySymbolRecord accessibleProperty)
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

}
