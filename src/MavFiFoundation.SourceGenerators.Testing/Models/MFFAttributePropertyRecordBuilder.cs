using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Testing.Models;

public class MFFAttributePropertyRecordBuilder
{
    #region Constants

    public const string DefaultName = "Name";
    public const object DefaultValue = null;
    public const MFFAttributePropertyLocationType DefaultFrom = 
        MFFAttributePropertyLocationType.Constructor;

    #endregion

    #region Private/Protected Fields/Properties

    private string _name = DefaultName; 
    private object? _value = DefaultValue;
    private MFFAttributePropertyLocationType _from = DefaultFrom;

    #endregion

    #region Public Methods

    public MFFAttributePropertyRecord Build()
    {
        return new MFFAttributePropertyRecord(_name, _value, _from);
    }

    public MFFAttributePropertyRecordBuilder Name(string name)
    {
        _name = name;
        return this;
    }

    public MFFAttributePropertyRecordBuilder Value(object? value)
    {
        _value = value;
        return this;
    }

    public MFFAttributePropertyRecordBuilder From(MFFAttributePropertyLocationType from)
    {
        _from = from;
        return this;
    }

    #endregion
}
