namespace MavFiFoundation.SourceGenerators.Models;

public class MFFGeneratorInfoModel
{
    public string? ContainingNamespace { get; set; }

    public string? SrcLocatorType { get; set; }

    public object? SrcLocatorInfo { get; set; }

    // Executes once with all sources
    public List<MFFBuilderModel>? GenOutputInfos { get; set; }

    // Executes once for each resource
    public List<MFFBuilderModel>? SrcOutputInfos  { get; set; }

}
