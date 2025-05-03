using System.Collections.Immutable;

using MavFiFoundation.SourceGenerators.GeneratorTriggers;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.ResourceLoaders;
using MavFiFoundation.SourceGenerators.Serializers;

namespace MavFiFoundation.SourceGenerators.UnitTests.GeneratorTriggers;

public class MFFAttributeGeneratorTriggerTestClass : MFFAttributeGeneratorTrigger
{
    public const string EXPOSED_CTOR_ARG_SRCLOCATORTYPE = CTOR_ARG_SRCLOCATORTYPE;
	public const string EXPOSED_CTOR_ARG_SRCLOCATORINFO = CTOR_ARG_SRCLOCATORINFO;
	public const string EXPOSED_CTOR_ARG_USESYMBOLFORLOCATORINFO = CTOR_ARG_USESYMBOLFORLOCATORINFO;

	public const string EXPOSED_CTOR_ARG_OUTPUTINFO = CTOR_ARG_OUTPUTINFO;

    public MFFAttributeGeneratorTriggerTestClass(IMFFSerializer serializer) : base(serializer)
    {

    }

    public ImmutableArray<MFFGeneratorInfoRecord?> ExposedGetTypesWithAttribute(
        ImmutableArray<MFFTypeSymbolSources> sourceAndTypes, 
        ImmutableArray<MFFResourceRecord> allResources,
        IEnumerable<IMFFResourceLoader> resourceLoaders,
        CancellationToken cancellationToken)
        {
            return base.GetTypesWithAttribute(
                sourceAndTypes, allResources, resourceLoaders,cancellationToken);
        }
}
