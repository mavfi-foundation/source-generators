using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Testing.Models;

public class MFFAttributePropertyRecordBuilder
{
    private string _name = "Name"; 
    private object? _value = null;
    private MFFAttributePropertyLocationType _from = MFFAttributePropertyLocationType.Constructor;

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
}
