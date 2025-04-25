using Microsoft.CodeAnalysis;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.ResourceLoaders;
using MavFiFoundation.SourceGenerators.Serializers;


namespace MavFiFoundation.SourceGenerators.GeneratorTriggers;

public abstract class MFFFileGeneratorTriggerBase : MFFGeneratorTriggerBase, IMFFGeneratorTrigger
{
    protected string FileNameSuffix { get; private set; }
    protected IMFFSerializer Serializer { get; private set; } 

    public MFFFileGeneratorTriggerBase(
        string name, 
        string fileNameSuffix, 
        IMFFSerializer serializer) : base(name) 
    { 
        FileNameSuffix = fileNameSuffix;
        Serializer = serializer;
    }

    public IncrementalValuesProvider<MFFGeneratorInfoRecord?> GetGeneratorInfosProvider(
        IncrementalGeneratorInitializationContext genContext,
        IncrementalValuesProvider<MFFTypeSymbolSources> allTypes, 
        IncrementalValuesProvider<MFFResourceRecord> allResources,
        IEnumerable<IMFFResourceLoader> resourceLoaders)
    {
        
        IncrementalValuesProvider<MFFGeneratorInfoRecord?> pipeline = allResources
        //FIXME: FileName suffix compare should be case insensitive
            .Where((resource) => resource.Name.EndsWith(FileNameSuffix))
            .Combine(allResources.Collect())
            .Select((combined, cancellationToken) =>
            {
                var resourcePath = combined.Left.Name;
                var sourceInfo = Deserialize(combined.Left.Text);

                if (sourceInfo is not null)
                {
                    if(string.IsNullOrWhiteSpace(sourceInfo.ContainingNamespace))
                    {
                        sourceInfo.ContainingNamespace = Path.GetDirectoryName(resourcePath)
                            ?.Replace(Path.DirectorySeparatorChar, '.') ?? string.Empty;
                    }

                    LoadResources(sourceInfo, combined.Right, resourceLoaders, cancellationToken);
                    return sourceInfo.ToRecord();
                }
                
                return null;
            });
            

        return pipeline;
    }

    protected virtual MFFGeneratorInfoModel? Deserialize(string serialized)
    {
        return Serializer.DeserializeObject<MFFGeneratorInfoModel>(serialized);
    }
}
