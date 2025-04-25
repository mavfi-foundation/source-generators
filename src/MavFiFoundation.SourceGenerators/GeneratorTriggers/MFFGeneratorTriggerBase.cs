using System;
using System.Collections.Immutable;

using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.ResourceLoaders;

namespace MavFiFoundation.SourceGenerators.GeneratorTriggers;

public abstract class MFFGeneratorTriggerBase : MFFGeneratorPluginBase
{
    public MFFGeneratorTriggerBase(string name) : base(name) { }

    protected void LoadResources(
        MFFGeneratorInfoModel genInfo,
        ImmutableArray<MFFResourceRecord> resources,
        IEnumerable<IMFFResourceLoader> resourceLoaders,
        CancellationToken cancellationToken)
    {
        object? srcLocatorInfo = genInfo.SrcLocatorInfo;

        foreach (var resourceLoader in resourceLoaders)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (resourceLoader.TryLoadResource(ref srcLocatorInfo, resources, cancellationToken))
            {
                genInfo.SrcLocatorInfo = srcLocatorInfo;
            }

            if(genInfo.SrcOutputInfos is not null)
            {
                foreach (var outputInfo in genInfo.SrcOutputInfos)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    
                    object? srcOutputInfo = outputInfo.SourceBuilderInfo;

                    if (resourceLoader.TryLoadResource(
                        ref srcOutputInfo, 
                        resources, 
                        cancellationToken))
                    {
                        outputInfo.SourceBuilderInfo = srcOutputInfo;
                    }
                }
            }
        }
    }
}
