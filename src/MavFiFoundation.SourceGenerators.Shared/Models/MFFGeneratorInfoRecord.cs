
namespace MavFiFoundation.SourceGenerators.Models;

public record MFFGeneratorInfoRecord
(
    string? ContainingNamespace,

    string SrcLocatorType,

    object SrcLocatorInfo,

    EquatableArray<MFFBuilderRecord> GenOutputInfos,

    EquatableArray<MFFBuilderRecord> SrcOutputInfos
);
