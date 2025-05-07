using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Testing.Models;

public class MFFTypeSymbolRecordBuilder
{
    #region Constants

    public const string DEFAULT_CONTAINING_NAMESPACE = "TestSpace";
    public const string DEFAULT_NAME = "Name";
    public const string DEFAULT_GENERIC_PARAMETERS = "";
    public const string DEFAULT_FULLY_QUALIFIED_NAME = "TestSpace.Name";
    public const string DEFAULT_CONSTAINTS = "";
    public const bool DEFAULT_IS_VALUE_TYPE = false;

    #endregion

    #region Private/Protected Fields/Properties

    private string _containingNamespace = DEFAULT_CONTAINING_NAMESPACE;
	private string _name = DEFAULT_NAME;
	private string _genericParameters = DEFAULT_GENERIC_PARAMETERS;
	private string _fullyQualifiedName = DEFAULT_FULLY_QUALIFIED_NAME;
	private string _constraints = DEFAULT_CONSTAINTS;
	private bool _isValueType = DEFAULT_IS_VALUE_TYPE;
	private IEnumerable<MFFPropertySymbolRecord> _accessibleProperties = Array.Empty<MFFPropertySymbolRecord>();
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

    #endregion
}
