using Scriban;

namespace MavFiFoundation.SourceGenerators.Builders;

public class MFFLiquidBuilder : MFFScribanBuilderBase
{
    public const string DEFAULT_NAME = nameof(MFFLiquidBuilder);

    public MFFLiquidBuilder() : base(DEFAULT_NAME) { }

    protected override Template Parse(string template)
    {
        return Template.ParseLiquid(template);
    }
}
