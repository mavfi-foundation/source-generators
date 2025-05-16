// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using Microsoft.CodeAnalysis;
using MavFiFoundation.SourceGenerators.Models;
using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.ResourceLoaders;
using MavFiFoundation.SourceGenerators.Serializers;

namespace MavFiFoundation.SourceGenerators.GeneratorTriggers;

/// <summary>
/// Used to trigger code generation based on an attribute included on a type. By default, 
/// <see cref="MFFGenerateSourceAttribute"/> is used.
/// </summary>
/// <example>
/// All information specified directly in attribute using <see cref="MFFJsonSerializer"/> to
/// deserialize builder information.
/// <code>
/// [MFFGenerateSource(MFFAttributeTypeLocator.DEFAULT_NAME,
///     "Examples.GenerateExampleAttribute",
/// """
/// [
/// 	{
/// 		"FileNameBuilderInfo": "{{ srcType.Name }}_Generated.g.cs", 
/// """ +
/// $"\n		\"SourceBuilderType\": \"{ MFFScribanBuilder.DEFAULT_NAME }\",\n" +
/// """
/// 		"SourceBuilderInfo": "#nullable enable\n\n
/// """ +
/// """
/// public partial class {{ srcType.Name }}_Generated\n{\n\n}"
/// """ + 
/// """
/// 	}
/// ]
/// """
/// 	)]
/// public class Example
/// {
/// 
/// }
/// </code>
/// </example>
/// <example>
/// Builder information specified in external source and loaded by <see cref="MFFResourceLoader"/>.
/// <code>
/// [MFFGenerateSource(MFFAttributeTypeLocator.DEFAULT_NAME,
///     "Examples.GenerateExampleAttribute",
///     "MFFResourceLoader:ExampleOutputInfo.res.json")]
/// public class Example
/// {
/// 
/// }
/// </code>
/// </example>
public class MFFAttributeGeneratorTrigger : MFFGeneratorTriggerBase, IMFFGeneratorTrigger
{
    #region Constants
    /// <summary>
    /// Default name used to identify the generator
    /// </summary>
    public const string DEFAULT_NAME = nameof(MFFAttributeGeneratorTrigger);

    /// <summary>
    /// Default attribute name used to locate triggering types and store generator
    /// configuration information.
    /// </summary>
    public const string DEFAULT_ATTRIBUTE_NAME = "MavFiFoundation.SourceGenerators.MFFGenerateSourceAttribute";


    /// <summary>
    /// Attribute constructor property name for srcLocatorType property
    /// </summary>
    protected const string CTOR_ARG_SRCLOCATORTYPE = "srcLocatorType";

    /// <summary>
    /// Attribute constructor property name for srcLocatorInfo property
    /// </summary>
    protected const string CTOR_ARG_SRCLOCATORINFO = "srcLocatorInfo";

    /// <summary>
    /// Attribute constructor property name for useSymbolForLocatorInfo property.
    /// </summary>
    protected const string CTOR_ARG_USESYMBOLFORLOCATORINFO = "useSymbolForLocatorInfo";

    /// <summary>
    /// Attribute constructor property name for outputInfo property
    /// </summary>
    protected const string CTOR_ARG_OUTPUTINFO = "outputInfo";

    #endregion

    #region Private/Protected Properties

    /// <summary>
    /// Gets the attribute name used to locate triggering types and store generator
    /// configuration information.
    /// </summary>
    protected string ConfigAttributeName { get; private set; }

    /// <summary>
    /// Get the default serializer to use.
    /// </summary>
    protected IMFFSerializer Serializer { get; private set; }


    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for <see cref="MFFAttributeGeneratorTrigger"/> that uses default attribute name.
    /// </summary>
    /// <param name="serializer">The default serializer to use.</param>
    public MFFAttributeGeneratorTrigger(IMFFSerializer serializer)
        : this(
            DEFAULT_NAME,
            DEFAULT_ATTRIBUTE_NAME,
            serializer)
    {
    }

    /// <summary>
    /// Constructor for <see cref="MFFAttributeGeneratorTrigger"/> that allows for a custom attribute
    /// to be used.
    /// </summary>
    /// <inheritdoc cref="MFFGeneratorPluginBase.MFFGeneratorPluginBase(string)" path="/param[@name='name']"/>
    /// <param name="configAttributeName">The attribute name used to locate triggering types and store generator
    /// configuration information.</param>
    /// <param name="serializer">The serializer to use.</param>
    public MFFAttributeGeneratorTrigger(string name, string configAttributeName, IMFFSerializer serializer) : base(name)
    {
        ConfigAttributeName = configAttributeName;
        Serializer = serializer;
    }

    #endregion

    #region IMFFGeneratorInfoLocator Implementation

    /// <inheritdoc/>
    public IncrementalValuesProvider<MFFGeneratorInfoRecord?> GetGeneratorInfosProvider(
        IncrementalGeneratorInitializationContext genContext,
        IncrementalValuesProvider<MFFTypeSymbolSources> allTypes,
        IncrementalValuesProvider<MFFResourceRecord> allResources,
        IEnumerable<IMFFResourceLoader> resourceLoaders)
    {
        var pipeline = allTypes.Collect().Combine(allResources.Collect())
            .SelectMany((combined, cancellationToken) =>
                GetTypesWithAttribute(
                    combined.Left,
                    combined.Right,
                    resourceLoaders,
                    cancellationToken));

        return pipeline;
    }

    /// <summary>
    /// Reads generator configuration information from all types that contain an attribute that
    /// matches <see cref="ConfigAttributeName"/> property.
    /// </summary>
    /// <inheritdoc cref="GetGeneratorInfosProvider" path="/param[@name='allTypes']"/>
    /// <inheritdoc cref="GetGeneratorInfosProvider" path="/param[@name='allResources']"/>
    /// <inheritdoc cref="GetGeneratorInfosProvider" path="/param[@name='resourceLoaders']"/>
    /// <inheritdoc cref="IMFFGeneratorHelper.ProcessNamespace" path="/param[@name='cancellationToken']"/>
    /// <returns>The <see cref="ImmutableArray{T}"/> containing generator configuration information.</returns>
    protected ImmutableArray<MFFGeneratorInfoRecord?> GetTypesWithAttribute(
        ImmutableArray<MFFTypeSymbolSources> allTypes,
        ImmutableArray<MFFResourceRecord> allResources,
        IEnumerable<IMFFResourceLoader> resourceLoaders,
        CancellationToken cancellationToken)
    {
        var generatorRecordsBuilder = ImmutableArray
                .CreateBuilder<MFFGeneratorInfoRecord?>();

        var selfSource = allTypes
            .Where(s => s.Source == MFFGeneratorConstants.Generator.COMPILING_PROJECT)
            .FirstOrDefault();

        if (selfSource is not null)
        {

            foreach (var srcType in selfSource.Types.Where(
                t => t.Attributes.Any(a => a.Name == ConfigAttributeName)))
            {
                cancellationToken.ThrowIfCancellationRequested();

                var att = srcType.Attributes.First(a => a.Name == ConfigAttributeName);
                var sourceInfo = new MFFGeneratorInfoModel();

                sourceInfo.ContainingNamespace = srcType.ContainingNamespace;

                foreach (var attProp in att.Properties
                    .Where(p => p.From == MFFAttributePropertyLocationType.Constructor))
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    switch (attProp.Name)
                    {
                        case CTOR_ARG_SRCLOCATORTYPE:
                            sourceInfo.SrcLocatorType = (string?)attProp.Value;
                            break;
                        case CTOR_ARG_SRCLOCATORINFO:
                            sourceInfo.SrcLocatorInfo = (string?)attProp.Value;
                            break;
                        case CTOR_ARG_USESYMBOLFORLOCATORINFO:
                            var useSymbol = attProp.Value == null ? false : (bool)attProp.Value;
                            if (useSymbol)
                            {
                                sourceInfo.SrcLocatorInfo = srcType;
                            }
                            break;
                        case CTOR_ARG_OUTPUTINFO:
                            var outputInfo = (string?)attProp.Value;
                            if (outputInfo is not null)
                            {
                                sourceInfo.SrcOutputInfos = Serializer.DeserializeObject<List<MFFBuilderModel>?>(outputInfo);
                            }
                            break;
                        default: //do nothing
                            break;
                    }
                }

                if (sourceInfo is not null)
                {
                    LoadResources(sourceInfo, allResources, resourceLoaders, cancellationToken);
                    generatorRecordsBuilder.Add(sourceInfo.ToRecord());
                }
            }
        }

        return generatorRecordsBuilder.ToImmutable();

    }

    #endregion
}
