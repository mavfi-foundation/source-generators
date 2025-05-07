using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.Serializers;
using MavFiFoundation.SourceGenerators.TypeLocators;

namespace MavFiFoundation.SourceGenerators.UnitTests.TypeLocators;

public class MFFAttributeTypeLocatorTestClass : MFFAttributeTypeLocator
{
    public MFFAttributeTypeLocatorTestClass(IMFFSerializer serializer) : base (serializer) 
    {

    }

    public ImmutableArray<MFFGeneratorInfoWithSrcTypesRecord?> ExposedGetTypesWithAttribute(
        ImmutableArray<MFFGeneratorInfoRecord?> genInfos, 
        ImmutableArray<MFFTypeSymbolSources> sources, 
        CancellationToken cancellationToken)
    {
        return GetTypesWithAttribute(genInfos, sources, cancellationToken);
    }
}
