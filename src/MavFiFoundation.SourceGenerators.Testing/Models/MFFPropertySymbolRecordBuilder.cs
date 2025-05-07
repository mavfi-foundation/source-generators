using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Testing.Models;

public class MFFPropertySymbolRecordBuilder
{
    #region Constants
 
    public const string DEFAULT_NAME = "Name";
    public const string DEFAULT_TYPE_FULLY_QUALIFIED_NAME= "System.String";
    public const bool DEFAULT_IS_INHERITED = false;
    public const bool DEFAULT_IS_VALUE_TYPE = false;
    public const bool DEFAULT_IS_NULLABLE = false;
    public const bool DEFAULT_IS_GENERIC_COLLECTION = false;

    #endregion

    #region Private/Protected Fields/Properties

    private string _name = DEFAULT_NAME;
    
    private string _typeFullyQualifiedName = DEFAULT_TYPE_FULLY_QUALIFIED_NAME;

    private bool _isInherited = DEFAULT_IS_INHERITED;

    private bool _isValueType = DEFAULT_IS_VALUE_TYPE;

    private bool _isNullable = DEFAULT_IS_NULLABLE;

    private bool _isGenericCollection = DEFAULT_IS_GENERIC_COLLECTION;

	private IEnumerable<MFFAttributeDataRecord> _attributes = Array.Empty<MFFAttributeDataRecord>();

    #endregion

    #region Public Methods

    public MFFPropertySymbolRecordBuilder Name(string name)
    {
        _name = name;
        return this;
    }

    public MFFPropertySymbolRecordBuilder TypeFullyQualifiedName(string typeFullyQualifiedName)
    {
        _typeFullyQualifiedName = typeFullyQualifiedName;
        return this;
    }

    public MFFPropertySymbolRecordBuilder IsInherited(bool isInherited)
    {
        _isInherited = isInherited;
        return this;
    }

    public MFFPropertySymbolRecordBuilder IsValueType(bool isValueType)
    {
        _isValueType = isValueType;
        return this;
    }

    public MFFPropertySymbolRecordBuilder IsNullable(bool isNullable)
    {
        _isNullable = isNullable;
        return this;
    }

    public MFFPropertySymbolRecordBuilder IsGenericCollection(bool isGenericCollection)
    {
        _isGenericCollection = isGenericCollection;
        return this;
    }

    public MFFPropertySymbolRecordBuilder Attributes(
        IEnumerable<MFFAttributeDataRecord> attributes)
    {
        _attributes = attributes;
        return this;
    }

    public MFFPropertySymbolRecordBuilder AddAttribute(MFFAttributeDataRecord attribute)
    {
        _attributes = _attributes.Append(attribute);
        return this;
    }

    public MFFPropertySymbolRecord Build()
    {
        return new MFFPropertySymbolRecord(
            _name, 
            _typeFullyQualifiedName, 
            _isInherited, 
            _isValueType, 
            _isNullable, 
            _isGenericCollection, 
            _attributes.ToImmutableArray());
    }

    #endregion
}
