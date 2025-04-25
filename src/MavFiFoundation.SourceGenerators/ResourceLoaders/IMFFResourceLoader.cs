using Microsoft.CodeAnalysis;
using MavFiFoundation.SourceGenerators.Models;
using System.Collections.Immutable;

namespace MavFiFoundation.SourceGenerators.ResourceLoaders;

public interface IMFFResourceLoader : IMFFGeneratorPlugin
{
    string Prefix { get; }  

    bool TryLoadResource(
        ref object? objResourceInfo, 
        ImmutableArray<MFFResourceRecord> allResources,
        CancellationToken cancellationToken);

}
