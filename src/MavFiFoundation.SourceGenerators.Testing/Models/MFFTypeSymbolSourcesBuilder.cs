using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Testing.Models;

public class MFFTypeSymbolSourcesBuilder
{
    private string _source = MFFGeneratorConstants.Generator.COMPILING_PROJECT; 
    private IEnumerable<MFFTypeSymbolRecord> _types = Array.Empty<MFFTypeSymbolRecord>();

    public MFFTypeSymbolSourcesBuilder Source(string source)
    {
        _source = source;
        return this;
    }

    public MFFTypeSymbolSourcesBuilder Types(IEnumerable<MFFTypeSymbolRecord> types)
    {
        _types = types;
        return this;
    }

    public MFFTypeSymbolSourcesBuilder AddType(MFFTypeSymbolRecord type)
    {
        _types = _types.Append(type);
        return this;
    }

    public MFFTypeSymbolSources Build()
    {
        return new MFFTypeSymbolSources(_source, _types.ToImmutableArray());
    }
}
