namespace MavFiFoundation.SourceGenerators.Models;

public record class MFFAttributePropertyRecord(
    string Name, 
    object? Value, 
    MFFAttributePropertyLocationType From);