using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.Builders;

namespace MavFiFoundation.SourceGenerators;

/// <summary>
/// Extendable abstract <see cref="IIncrementalGenerator"/> base class.
/// </summary>
public abstract class MFFGeneratorBase : IIncrementalGenerator
{

    #region Private/Protected Properties

    /// <summary>
    /// Gets the <see cref="IMFFGeneratorPluginsProvider"/> used to provide the generator plugins.
    /// </summary>
    protected IMFFGeneratorPluginsProvider PluginsProvider { get; private set; }

    /// <summary>
    /// Gets the <see cref="IMFFGeneratorHelper"/> used to provide helper methods for the generator.
    /// </summary>
    protected IMFFGeneratorHelper GeneratorHelper { get; private set; }

    #endregion

    #region Constructors
    
    /// <param name="pluginsProvider">The <see cref="IMFFGeneratorPluginsProvider"/> used to provide the generator plugins.</param>
    /// <param name="generatorHelper">The <see cref="IMFFGeneratorHelper"/> used to provide helper methods for the generator.</param>
    public MFFGeneratorBase(
        IMFFGeneratorPluginsProvider pluginsProvider,
        IMFFGeneratorHelper generatorHelper)
    {
        PluginsProvider = pluginsProvider;
        GeneratorHelper = generatorHelper;
    }

    #endregion

    #region IIncrementalGenerator Implementation
 
    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext initContext)
    {
        var allTypes = GeneratorHelper.GetAllTypesProvider(initContext);
        var allResources = GeneratorHelper.GetAllResourcesProvider(initContext);
        var genInfos = GeneratorHelper.GetGenerateConstantsProvider(initContext, PluginsProvider);

        foreach (var configLocator in PluginsProvider.GeneratorTriggers)
        {
            var additionalSrcInfos = configLocator.Value
                .GetGeneratorInfosProvider(
                    initContext,
                    allTypes,
                    allResources,
                    PluginsProvider.ResourceLoaders.Values);

            var collected = additionalSrcInfos.Collect();
            genInfos = collected.Combine(
                genInfos.Collect()).SelectMany((combinedGenInfos, cancellationToken) =>
            {
                return combinedGenInfos.Left.AddRange(combinedGenInfos.Right);
            });
        }

        IncrementalValuesProvider<MFFGeneratorInfoWithSrcTypesRecord?>? genInfosWithTypes = null;

        foreach (var sourceTypeLocator in PluginsProvider.TypeLocators)
        {

            var filteredSrcInfos = genInfos.Where(srcInfo =>
            {
                return srcInfo?.SrcLocatorType == sourceTypeLocator.Key;
            });

            var additionalGenInfosWithTypes = sourceTypeLocator.Value.GetTypeSymbolsProvider(
                initContext, filteredSrcInfos, allTypes);

            if (genInfosWithTypes != null && genInfosWithTypes.HasValue)
            {
                var collected = additionalGenInfosWithTypes.Collect();
                genInfosWithTypes = collected
                    .Combine(genInfosWithTypes.Value.Collect())
                    .SelectMany((combinedGenInfosWithTypes, cancellationToken) =>
                    {
                        return combinedGenInfosWithTypes.Left.AddRange(combinedGenInfosWithTypes.Right);
                    });
            }
            else
            {

                genInfosWithTypes = additionalGenInfosWithTypes;
            }
        }

        if (genInfosWithTypes != null && genInfosWithTypes.HasValue)
        {
            initContext.RegisterSourceOutput(genInfosWithTypes.Value.Collect(),
                (context, source) => CreateOutput(source, context));
        }
    }

    /// <summary>
    /// Creates the output for the generator.
    /// </summary>
    /// <param name="genAndSrcInfos">Generator configuration and located source types to output sources for.</param>
    /// <param name="context">Source production context to use.</param>
    protected void CreateOutput(
        ImmutableArray<MFFGeneratorInfoWithSrcTypesRecord?> genAndSrcInfos,
        SourceProductionContext context)
    {
        foreach (var genAndSrcInfo in genAndSrcInfos)
        {
            context.CancellationToken.ThrowIfCancellationRequested();

            if (genAndSrcInfo is not null)
            {
                if (!genAndSrcInfo.GenInfo.GenOutputInfos.IsDefaultOrEmpty)
                {
                    foreach (var outputInfo in genAndSrcInfo.GenInfo.GenOutputInfos)
                    {
                        context.CancellationToken.ThrowIfCancellationRequested();

                        IMFFBuilder builder;

                        if (PluginsProvider.Builders.TryGetValue(
                            outputInfo.SourceBuilderType, out builder))
                        {
                            string code = builder.Build(outputInfo.SourceBuilderInfo, outputInfo, genAndSrcInfo.SrcTypes);

                            IMFFBuilder fileNameBuilder = GetFileNameBuilder(outputInfo);

                            string fileName = fileNameBuilder.Build(
                                outputInfo.FileNameBuilderInfo,
                                outputInfo,
                                genAndSrcInfo.SrcTypes
                            );

                            context.AddSource(fileName, SourceText.From(code, Encoding.UTF8));
                        }
                    }
                }

                // Create an array of builder that matches the Configured builders for srcTypes 
                // so don't need to do lookup for every srcType 
                var builders = genAndSrcInfo.GenInfo.SrcOutputInfos
                    .Select(bc => PluginsProvider.Builders[bc.SourceBuilderType])
                    .ToArray();

                foreach (var srcType in genAndSrcInfo.SrcTypes)
                {
                    context.CancellationToken.ThrowIfCancellationRequested();

                    if (srcType is not null && !genAndSrcInfo.GenInfo.SrcOutputInfos.IsDefaultOrEmpty)
                    {
                        for (int i = 0; i < genAndSrcInfo.GenInfo.SrcOutputInfos.Length; i++)
                        {
                            context.CancellationToken.ThrowIfCancellationRequested();

                            var outputInfo = genAndSrcInfo.GenInfo.SrcOutputInfos[i];
                            var builder = builders[i];

                            if (builder is not null)
                            {
                                string code = builder.Build(outputInfo.SourceBuilderInfo, outputInfo, srcType);

                                IMFFBuilder fileNameBuilder = GetFileNameBuilder(outputInfo);

                                string fileName = fileNameBuilder.Build(
                                            outputInfo.FileNameBuilderInfo,
                                            outputInfo,
                                            srcType
                                        );

                                context.AddSource(fileName, SourceText.From(code, Encoding.UTF8));
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Gets the file name builder for the given <see cref="MFFBuilderRecord"/>.
    /// </summary>
    /// <param name="outputInfo">The <see cref="MFFBuilderRecord"/> to get the file name builder for.</param>
    private IMFFBuilder GetFileNameBuilder(MFFBuilderRecord outputInfo)
    {
        IMFFBuilder fileNameBuilder;

        if (outputInfo.FileNameBuilderType is null || string.IsNullOrEmpty(outputInfo.FileNameBuilderType))
        {
            if (PluginsProvider.DefaultFileNameBuilder != null)
            {
                fileNameBuilder = PluginsProvider.DefaultFileNameBuilder;
            }
            else
            {
                throw new Exception(
                    $"{nameof(outputInfo.FileNameBuilderType)} not provided and no {nameof(PluginsProvider.DefaultFileNameBuilder)} is set.");
            }
        }
        else
        {

            if (!PluginsProvider.Builders.TryGetValue(outputInfo.FileNameBuilderType, out fileNameBuilder))
            {
                throw new KeyNotFoundException($"'outputInfo.FileNameBuilderType' not found in {nameof(PluginsProvider.Builders)}");
            }
        }

        return fileNameBuilder;
    }
    #endregion
}
