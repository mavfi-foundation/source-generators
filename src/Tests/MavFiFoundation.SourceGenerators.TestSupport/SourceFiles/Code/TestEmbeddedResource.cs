using MavFiFoundation.SourceGenerators;

namespace TestSpace;

[MFFEmbeddedResource]
public static class TestEmbeddedResource
{
    public const string RESOURCE = 
"""
#nullable enable

public partial class {{ srcType.Name }}_Generated
{

}
""";
}
