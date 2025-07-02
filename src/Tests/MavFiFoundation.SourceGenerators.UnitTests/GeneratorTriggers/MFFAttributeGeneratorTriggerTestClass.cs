// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.GeneratorTriggers;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.ResourceLoaders;
using MavFiFoundation.SourceGenerators.Serializers;

namespace MavFiFoundation.SourceGenerators.UnitTests.GeneratorTriggers;

public class MFFAttributeGeneratorTriggerTestClass : MFFAttributeGeneratorTrigger
{
    public const string ExposedCtorArgSrcLocatorType = CtorArgSrcLocatorType;
	public const string ExposedCtorArgSrcLocatorInfo = CtorArgSrcLocatorInfo;
	public const string ExposedCtorArgUseSymbolForLocatorInfo = CtorArgUseSymbolForLocatorInfo;

	public const string ExposedCtroArgOutoutInfo = CtorArgSrcOutputInfo;

    public MFFAttributeGeneratorTriggerTestClass(
        IMFFGeneratorPluginsProvider pluginsProvider,
        IMFFSerializer serializer) : base(pluginsProvider, serializer)
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
