// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.ResourceLoaders;

namespace MavFiFoundation.SourceGenerators.GeneratorTriggers;

/// <summary>
/// The base class that all <see cref="IMFFGeneratorTrigger"/> implementations SHOULD implement.
/// </summary>
public abstract class MFFGeneratorTriggerBase : MFFGeneratorPluginBase
{
    /// <summary>
    /// Constructor for <see cref="MFFGeneratorTriggerBase"/>
    /// </summary>
    /// <inheritdoc cref="MFFGeneratorPluginBase.MFFGeneratorPluginBase(string)" path="/param[@name='name']"/>
    public MFFGeneratorTriggerBase(string name) : base(name) { }

    /// <summary>
    /// Used to load properties on an <see cref="MFFGeneratorInfoModel"/> from external sources.
    /// </summary>
    /// <inheritdoc cref="IMFFGeneratorTrigger.GetGeneratorInfosProvider(Microsoft.CodeAnalysis.IncrementalGeneratorInitializationContext, Microsoft.CodeAnalysis.IncrementalValuesProvider{MFFTypeSymbolSources}, Microsoft.CodeAnalysis.IncrementalValuesProvider{MFFResourceRecord}, IEnumerable{IMFFResourceLoader})" path="/param[@name='allResources']"/>
    /// <inheritdoc cref="IMFFGeneratorTrigger.GetGeneratorInfosProvider(Microsoft.CodeAnalysis.IncrementalGeneratorInitializationContext, Microsoft.CodeAnalysis.IncrementalValuesProvider{MFFTypeSymbolSources}, Microsoft.CodeAnalysis.IncrementalValuesProvider{MFFResourceRecord}, IEnumerable{IMFFResourceLoader})" path="/param[@name='resourceLoaders']"/>
    /// <param name="genInfo">Generator configuration information.</param>
    /// <inheritdoc cref="IMFFGeneratorHelper.ProcessNamespace" path="/param[@name='cancellationToken']"/>
    protected virtual void LoadResources(
        MFFGeneratorInfoModel genInfo,
        ImmutableArray<MFFResourceRecord> allResources,
        IEnumerable<IMFFResourceLoader> resourceLoaders,
        CancellationToken cancellationToken)
    {
        object? srcLocatorInfo = genInfo.SrcLocatorInfo;

        foreach (var resourceLoader in resourceLoaders)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (resourceLoader.TryLoadResource(ref srcLocatorInfo, allResources, cancellationToken))
            {
                genInfo.SrcLocatorInfo = srcLocatorInfo;
            }

            if (genInfo.SrcOutputInfos is not null)
            {
                foreach (var outputInfo in genInfo.SrcOutputInfos)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    object? srcOutputInfo = outputInfo.SourceBuilderInfo;

                    if (resourceLoader.TryLoadResource(
                        ref srcOutputInfo,
                        allResources,
                        cancellationToken))
                    {
                        outputInfo.SourceBuilderInfo = srcOutputInfo;
                    }
                }
            }
        }
    }
}
