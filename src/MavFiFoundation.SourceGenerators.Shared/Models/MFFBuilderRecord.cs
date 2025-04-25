
namespace MavFiFoundation.SourceGenerators.Models;

public record MFFBuilderRecord(

    string? FileNameBuilderType,
    string FileNameBuilderInfo,
    string SourceBuilderType,
    object SourceBuilderInfo,
    EquatableArray<(string Key, object Value)> AdditionalOutputInfos
    
//TODO: Diff Path for templates support - Maybe added to AdditionalOutputInfos - need to consider loading from resources
);
