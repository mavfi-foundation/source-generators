using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Testing.Models;

public class MFFAttributeDataRecordBuilder
{
    #region Constants

    public const string DEFAULT_NAME = "Name";

    #endregion

    #region Private/Protected Fields/Properties

    private string _name = DEFAULT_NAME;

    private IEnumerable<MFFAttributePropertyRecord> _properties = 
        Array.Empty<MFFAttributePropertyRecord>();

    #endregion

    #region Public Methods

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
    #endregion
}
