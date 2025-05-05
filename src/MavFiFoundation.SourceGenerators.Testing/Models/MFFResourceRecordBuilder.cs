using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Testing.Models;

public class MFFResourceRecordBuilder
{
    private string _name = "Name";
    private string _text = "Text";

    public MFFResourceRecordBuilder Name(string name)
    {
        _name = name;
        return this;
    }

    public MFFResourceRecordBuilder Text(string text)
    {
        _text = text;
        return this;
    }

    public MFFResourceRecord Build()
    {
        return new MFFResourceRecord(_name, _text);
    }
}
