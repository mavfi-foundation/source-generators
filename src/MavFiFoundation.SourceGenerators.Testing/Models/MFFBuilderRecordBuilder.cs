using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Testing.Models;

public class MFFBuilderRecordBuilder
{
    #region Constants

    public const string DEFAULT_FILE_NAME_BUILDER_INFO = "TestClass.cs";
    public const string DEFAULT_SOURCE_BUILDER_TYPE = "SouceBuilderType";
    public const string DEFAULT_SOURCE_BUILDER_INFO = "SouceBuilderInfo";

    #endregion

    #region Private/Protected Fields/Properties

    private string? _fileNameBuilderType;
    private string _fileNameBuilderInfo = DEFAULT_FILE_NAME_BUILDER_INFO;
    private string _sourceBuilderType = DEFAULT_SOURCE_BUILDER_TYPE;
    private object _sourceBuilderInfo = DEFAULT_SOURCE_BUILDER_INFO;
    private IEnumerable<(string Key, object Value)> _additionalOutputInfos = 
        Array.Empty<(string Key, object Value)>();

    #endregion

    #region Public Methods

    public MFFBuilderRecordBuilder FileNameBuilderType(string? fileNameBuilderType)
    {
        _fileNameBuilderType = fileNameBuilderType;
        return this;
    }

    public MFFBuilderRecordBuilder FileNameBuilderInfo(string fileNameBuilderInfo)
    {
        _fileNameBuilderInfo = fileNameBuilderInfo;
        return this;
    }

    public MFFBuilderRecordBuilder SourceBuilderType(string sourceBuilderType)
    {
        _sourceBuilderType = sourceBuilderType;
        return this;
    }

    public MFFBuilderRecordBuilder SourceBuilderInfo(string sourceBuilderInfo)
    {
        _sourceBuilderInfo = sourceBuilderInfo;
        return this;
    }

    public MFFBuilderRecordBuilder AdditionalOutputInfos(
        IEnumerable<(string Key, object Value)> additionalOutputInfos)
    {
        _additionalOutputInfos = additionalOutputInfos;
        return this;
    }

    public MFFBuilderRecordBuilder AddAdditionalOutputInfo(
        (string Key, object Value) additionalOutputInfo)
    {
        _additionalOutputInfos = _additionalOutputInfos.Append(additionalOutputInfo);
        return this;
    }

    public MFFBuilderRecordBuilder AddAdditionalOutputInfo(
        string key, object value)
    {
        return AddAdditionalOutputInfo((Key: key, Value: value));
    }

    public MFFBuilderRecord Build()
    {
        return new MFFBuilderRecord(
            _fileNameBuilderType, 
            _fileNameBuilderInfo, 
            _sourceBuilderType, 
            _sourceBuilderInfo,
            _additionalOutputInfos.ToImmutableArray());
    }

    #endregion
}
