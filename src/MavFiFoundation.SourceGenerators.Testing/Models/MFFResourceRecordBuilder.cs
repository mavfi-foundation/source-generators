using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Testing.Models;

public class MFFResourceRecordBuilder
{
    #region Constants

    public const string DEFAULT_NAME = "Name";
    public const string DEFAULT_TEXT = "Text";

    #endregion

    #region Private/Protected Fields/Properties

    private string _name = DEFAULT_NAME;
    private string _text = DEFAULT_TEXT;

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
