using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Testing.Models;

public class MFFAttributePropertyRecordBuilder
{
    #region Constants

    public const string DEFAULT_NAME = "Name";
    public const object DEFAULT_VALUE = null;
    public const MFFAttributePropertyLocationType DEFAULT_FROM = 
        MFFAttributePropertyLocationType.Constructor;

    #endregion

    #region Private/Protected Fields/Properties

    private string _name = DEFAULT_NAME; 
    private object? _value = DEFAULT_VALUE;
    private MFFAttributePropertyLocationType _from = DEFAULT_FROM;

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
