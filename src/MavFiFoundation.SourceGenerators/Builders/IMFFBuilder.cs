using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Builders;

public interface IMFFBuilder : IMFFGeneratorPlugin
{
    string Build(object templateInfo,
                        MFFBuilderRecord builderRec, 
                        IEnumerable<MFFTypeSymbolRecord> srcTypes);

    string Build(object templateInfo,
                        MFFBuilderRecord builderRec, 
                        MFFTypeSymbolRecord srcType);
}
