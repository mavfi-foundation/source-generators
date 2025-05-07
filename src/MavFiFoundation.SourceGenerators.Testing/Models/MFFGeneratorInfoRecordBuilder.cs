using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Testing.Models;

public class MFFGeneratorInfoRecordBuilder
{
    #region Constants

    public const string DEFAULT_CONTAINING_NAMESPACE = "TestSpace";
    public const string DEFAULT_SRC_LOCATOR_TYPE = "SrcLocatorType";
    public const string DEFAULT_SRC_LOCATOR_INFO = "SrcLocatorInfo";

    #endregion

    #region Private/Protected Fields/Properties

    private string? _containingNamespace = DEFAULT_CONTAINING_NAMESPACE;

    private string _srcLocatorType = DEFAULT_SRC_LOCATOR_TYPE;

    private object _srcLocatorInfo = DEFAULT_SRC_LOCATOR_INFO;

    private IEnumerable<MFFBuilderRecord> _genOutputInfos = Array.Empty<MFFBuilderRecord>();

    private IEnumerable<MFFBuilderRecord> _srcOutputInfos = Array.Empty<MFFBuilderRecord>();

    #endregion

    #region Public Methods

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

    #endregion
}
