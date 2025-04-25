using Microsoft.CodeAnalysis;

namespace MavFiFoundation.SourceGenerators;

[Generator]

public class MFFGenerator : MFFGeneratorBase
{
    public MFFGenerator () : base(
        new MFFGeneratorPluginsProvider(),
        new MFFGeneratorHelper())
    {
        
    }
}
