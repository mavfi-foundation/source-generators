namespace MavFiFoundation.SourceGenerators.Models;

public record class MFFTypeSymbolSources(
    string Source, 
    EquatableArray<MFFTypeSymbolRecord> Types
);

