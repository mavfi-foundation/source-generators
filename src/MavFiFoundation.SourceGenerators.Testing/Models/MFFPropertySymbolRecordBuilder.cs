using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Testing.Models;

public class MFFPropertySymbolRecordBuilder
{
    private string _name = "Name";
    
    private string _typeFullyQualifiedName = "System.String";

    private bool _isInherited = false;

    private bool _isValueType = false;

    private bool _isNullable = false;

    private bool _isGenericCollection = false;

	private IEnumerable<MFFAttributeDataRecord> _attributes = Array.Empty<MFFAttributeDataRecord>();

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
}
