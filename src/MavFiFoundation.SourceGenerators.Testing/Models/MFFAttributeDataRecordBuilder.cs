using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MavFiFoundation.SourceGenerators.Testing.Models;

public class MFFAttributeDataRecordBuilder
{
    public const string DEFAULT_NAME = "Name";
    private string _name = DEFAULT_NAME;

    private IEnumerable<MFFAttributePropertyRecord> _properties = 
        Array.Empty<MFFAttributePropertyRecord>();

    public MFFAttributeDataRecordBuilder Name(string name)
    {
        _name = name;
        return this;
    }

    public MFFAttributeDataRecordBuilder Properties(IEnumerable<MFFAttributePropertyRecord> properties)
    {
        _properties = properties;
        return this;
    }

    public MFFAttributeDataRecordBuilder AddProperty(MFFAttributePropertyRecord property)
    {
        _properties = _properties.Append(property);
        return this;
    }

    public MFFAttributeDataRecord Build()
    {
        return new MFFAttributeDataRecord(_name, _properties.ToImmutableArray());
    }
}
