
namespace MavFiFoundation.SourceGenerators.Models;

public class MFFTypeLocatorInfoBase
{
    public string[] Assemblies2Search { get; set; } = Array.Empty<string>();

    public string[] Types2Exclude {get; set;} = Array.Empty<string>();

    public bool NoSearchProjectTypes {get; set;}
}
