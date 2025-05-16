// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using Microsoft.CodeAnalysis;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.ResourceLoaders;
using MavFiFoundation.SourceGenerators.Serializers;
using System.Text.RegularExpressions;
using System.Collections.Immutable;


namespace MavFiFoundation.SourceGenerators.GeneratorTriggers;

/// <summary>
/// A base class that <see cref="IMFFGeneratorTrigger"/> implementations that are based on
/// configuration files MAY use.
/// </summary>
public abstract class MFFFileGeneratorTriggerBase : MFFGeneratorTriggerBase, IMFFGeneratorTrigger
{
    /// <summary>
    /// Gets a regex string containing the file name suffix used to locate triggering files.
    /// </summary>
    protected string FileNameSuffix { get; private set; }

    /// <summary>
    /// Gets a <see cref="Regex"/> created using <see cref="FileNameSuffix"/>.
    /// </summary>
    protected Regex FileNameSuffixRegex { get; private set; }

    /// <summary>
    /// Get the default serializer to use.
    /// </summary>
    protected IMFFSerializer Serializer { get; private set; }

    /// <inheritdoc cref="MFFGeneratorPluginBase.MFFGeneratorPluginBase(string)" path="/param[@name='name']"/>
    /// <param name="fileNameSuffix">A regex string containing the file name suffix used to locate triggering files.</param>
    /// <param name="serializer">The default serializer to use.</param>
    public MFFFileGeneratorTriggerBase(
        string name,
        string fileNameSuffix,
        IMFFSerializer serializer) : base(name)
    {
        FileNameSuffix = fileNameSuffix;
        FileNameSuffixRegex = new Regex($"{fileNameSuffix}$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        Serializer = serializer;
    }

    /// <inheritdoc/>
    public IncrementalValuesProvider<MFFGeneratorInfoRecord?> GetGeneratorInfosProvider(
        IncrementalGeneratorInitializationContext genContext,
        IncrementalValuesProvider<MFFTypeSymbolSources> allTypes,
        IncrementalValuesProvider<MFFResourceRecord> allResources,
        IEnumerable<IMFFResourceLoader> resourceLoaders)
    {

        IncrementalValuesProvider<MFFGeneratorInfoRecord?> pipeline = allResources
            .Where((resource) => FileNameSuffixRegex.IsMatch(resource.Name))
            .Combine(allResources.Collect())
            .Select((combined, cancellationToken) =>
                 GetGeneratorInfoFromFile(resourceLoaders, combined.Left, combined.Right, cancellationToken));

        return pipeline;
    }

    /// <summary>
    /// Loads generator configuration from the provided resource.
    /// </summary>
    /// <inheritdoc cref="GetGeneratorInfosProvider" path="/param[@name='resourceLoaders']"/>
    /// <param name="resource">file to load generator information from.</param>
    /// <inheritdoc cref="GetGeneratorInfosProvider" path="/param[@name='allResources']"/>
    /// <inheritdoc cref="IMFFGeneratorHelper.ProcessNamespace" path="/param[@name='cancellationToken']"/>
    /// <returns>The created <see cref="MFFGeneratorInfoRecord"/></returns>
    protected MFFGeneratorInfoRecord? GetGeneratorInfoFromFile(
        IEnumerable<IMFFResourceLoader> resourceLoaders,
        MFFResourceRecord resource,
        ImmutableArray<MFFResourceRecord> allResources,
        CancellationToken cancellationToken)
    {
        var resourcePath = resource.Name;
        var sourceInfo = Serializer.DeserializeObject<MFFGeneratorInfoModel>(resource.Text);

        if (sourceInfo is not null)
        {
            if (string.IsNullOrWhiteSpace(sourceInfo.ContainingNamespace))
            {
                sourceInfo.ContainingNamespace = Path.GetDirectoryName(resourcePath)
                    ?.Replace(Path.DirectorySeparatorChar, '.') ?? string.Empty;
            }

            LoadResources(sourceInfo, allResources, resourceLoaders, cancellationToken);
            return sourceInfo.ToRecord();
        }

        return null;
    }
}
