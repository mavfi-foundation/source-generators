
using System.Xml.Serialization;

namespace MavFiFoundation.SourceGenerators.Models;

public class MFFBuilderModel
{
    public string? FileNameBuilderType  { get; set; }
    public string? FileNameBuilderInfo { get; set; }
    public string? SourceBuilderType { get; set; }
    public object? SourceBuilderInfo { get; set; }
    public Dictionary<string, object>? AdditionalOutputInfos { get; set; }
}
