using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Testing.Models;

public class MFFGeneratorInfoWithSrcTypesRecordBuilder
{
    private MFFGeneratorInfoRecord _genInfo = new MFFGeneratorInfoRecordBuilder().Build();
    private IEnumerable<MFFTypeSymbolRecord> _srcTypes = Array.Empty<MFFTypeSymbolRecord>();

    public MFFGeneratorInfoWithSrcTypesRecordBuilder GenInfo(MFFGeneratorInfoRecord genInfo)
    {
        _genInfo = genInfo;
        return this;
    }

    public MFFGeneratorInfoWithSrcTypesRecordBuilder SrcTypes(IEnumerable<MFFTypeSymbolRecord> srcTypes)
    {
        _srcTypes = srcTypes;
        return this;
    }

    public MFFGeneratorInfoWithSrcTypesRecordBuilder AddSrcType(MFFTypeSymbolRecord srcType)
    {
        _srcTypes = _srcTypes.Append(srcType);
        return this;
    }

    public MFFGeneratorInfoWithSrcTypesRecord Build()
    {
        return new MFFGeneratorInfoWithSrcTypesRecord(_genInfo, _srcTypes.ToImmutableArray());
    }
}
