using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using MavFiFoundation.SourceGenerators.Models;
using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.ResourceLoaders;

namespace MavFiFoundation.SourceGenerators.GeneratorTriggers;

public class MFFAttributeGeneratorTrigger : MFFGeneratorTriggerBase, IMFFGeneratorTrigger
{
	#region Constants
    public const string DEFAULT_NAME = nameof(MFFAttributeGeneratorTrigger);

	public const string DEFAULT_ATTRIBUTE_NAME = "MavFiFoundation.SourceGenerators.MFFGenerateSourceAttribute";


	private const string CTOR_ARG_SRCLOCATORTYPE = "srcLocatorType";
	private const string CTOR_ARG_SRCLOCATORINFO = "srcLocatorInfo";
	private const string CTOR_ARG_USESYMBOLFORLOCATORINFO = "useSymbolForLocatorInfo";

	private const string CTOR_ARG_OUTPUTINFO = "outputInfo";

	#endregion

	#region Private/Protected Properties

	protected string ConfigAttributeName { get; private set; }

	#endregion

	#region Constructors
    public MFFAttributeGeneratorTrigger() 
		: this(
			DEFAULT_NAME, 
			DEFAULT_ATTRIBUTE_NAME)
    {
    }

    public MFFAttributeGeneratorTrigger(string name, string configAttributeName) : base(name)
    {
        ConfigAttributeName = configAttributeName;
    }

	#endregion

	#region IMFFGeneratorInfoLocator Implementation
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

    protected ImmutableArray<MFFGeneratorInfoRecord?> GetTypesWithAttribute(
        ImmutableArray<MFFTypeSymbolSources> sourceAndTypes, 
        ImmutableArray<MFFResourceRecord> allResources,
        IEnumerable<IMFFResourceLoader> resourceLoaders,
        CancellationToken cancellationToken)
    {
		var generatorRecordsBuilder = ImmutableArray
				.CreateBuilder<MFFGeneratorInfoRecord?>();

		var selfSource = sourceAndTypes
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
                                //TODO: Use IMFFSerializer - Special handling for List
								sourceInfo.SrcOutputInfos = JsonConvert.DeserializeObject<List<MFFBuilderModel>?>(outputInfo);
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
