using Scriban;

namespace MavFiFoundation.SourceGenerators.Builders;

public class MFFScribanBuilder : MFFScribanBuilderBase
{
    public const string DEFAULT_NAME = nameof(MFFScribanBuilder);
    public MFFScribanBuilder() : base(DEFAULT_NAME) { }

    protected override Template Parse(string template)
    {
        return Template.Parse(template);
    }
}
