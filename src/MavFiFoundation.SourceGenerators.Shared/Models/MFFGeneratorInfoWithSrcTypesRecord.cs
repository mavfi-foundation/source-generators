namespace MavFiFoundation.SourceGenerators.Models;

public record class MFFGeneratorInfoWithSrcTypesRecord(
    MFFGeneratorInfoRecord GenInfo, 
    EquatableArray<MFFTypeSymbolRecord> SrcTypes
);
