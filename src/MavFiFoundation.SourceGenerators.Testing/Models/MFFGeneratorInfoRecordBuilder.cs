using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Testing.Models;

public class MFFGeneratorInfoRecordBuilder
{
    private string? _containingNamespace = "TestSpace";

    private string _srcLocatorType = "SrcLocatorType";

    private object _srcLocatorInfo = "SrcLocatorInfo";

    private IEnumerable<MFFBuilderRecord> _genOutputInfos = Array.Empty<MFFBuilderRecord>();

    private IEnumerable<MFFBuilderRecord> _srcOutputInfos = Array.Empty<MFFBuilderRecord>();

    public MFFGeneratorInfoRecordBuilder ContainingNamespace(string? containingNamespace)
    {
        _containingNamespace = containingNamespace;
        return this;
    }

    public MFFGeneratorInfoRecordBuilder SrcLocatorType(string srcLocatorType)
    {
        _srcLocatorType = srcLocatorType;
        return this;
    }

    public MFFGeneratorInfoRecordBuilder SrcLocatorInfo(object srcLocatorInfo)
    {
        _srcLocatorInfo = srcLocatorInfo;
        return this;
    }

    public MFFGeneratorInfoRecordBuilder GenOutputInfos(IEnumerable<MFFBuilderRecord> genOutputInfos)
    {
        _genOutputInfos = genOutputInfos;
        return this;
    }

    public MFFGeneratorInfoRecordBuilder AddGenOutputInfo(MFFBuilderRecord genOutputInfo)
    {
        _genOutputInfos = _genOutputInfos.Append(genOutputInfo);
        return this;
    }

    public MFFGeneratorInfoRecordBuilder SrcOutputInfos(IEnumerable<MFFBuilderRecord> srcOutputInfos)
    {
        _srcOutputInfos = srcOutputInfos;
        return this;
    }

    public MFFGeneratorInfoRecordBuilder AddSrcOutputInfo(MFFBuilderRecord srcOutputInfo)
    {
        _srcOutputInfos = _srcOutputInfos.Append(srcOutputInfo);
        return this;
    }

    public MFFGeneratorInfoRecord Build()
    {
        return new MFFGeneratorInfoRecord(
            _containingNamespace, 
            _srcLocatorType, 
            _srcLocatorInfo, 
            _genOutputInfos.ToImmutableArray(), 
            _srcOutputInfos.ToImmutableArray());
    }
}
