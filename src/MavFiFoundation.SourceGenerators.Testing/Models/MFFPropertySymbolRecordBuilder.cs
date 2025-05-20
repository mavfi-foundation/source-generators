using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Testing.Models;

public class MFFPropertySymbolRecordBuilder
{
    #region Constants
 
    public const string DefaultName = "Name";
    public const string DefaultTypeFullyQualifiedName= "System.String";
    public const bool DefaultIsInherited = false;
    public const bool DefaultIsValueType = false;
    public const bool DefaultIsNullable = false;
    public const bool DefaultIsGenericCollection = false;

    #endregion

    #region Private/Protected Fields/Properties

    private string _name = DefaultName;
    
    private string _typeFullyQualifiedName = DefaultTypeFullyQualifiedName;

    private bool _isInherited = DefaultIsInherited;

    private bool _isValueType = DefaultIsValueType;

    private bool _isNullable = DefaultIsNullable;

    private bool _isGenericCollection = DefaultIsGenericCollection;

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
