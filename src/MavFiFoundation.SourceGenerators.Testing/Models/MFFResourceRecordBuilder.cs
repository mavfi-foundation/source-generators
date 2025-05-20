using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Testing.Models;

public class MFFResourceRecordBuilder
{
    #region Constants

    public const string DefaultName = "Name";
    public const string DefaultText = "Text";

    #endregion

    #region Private/Protected Fields/Properties

    private string _name = DefaultName;
    private string _text = DefaultText;

    #endregion

    #region Public Methods

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

    #endregion
}
