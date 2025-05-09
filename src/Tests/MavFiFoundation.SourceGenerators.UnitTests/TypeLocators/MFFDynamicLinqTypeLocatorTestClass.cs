using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.Serializers;
using MavFiFoundation.SourceGenerators.TypeLocators;

namespace MavFiFoundation.SourceGenerators.UnitTests.TypeLocators;

public class MFFDynamicLinqTypeLocatorTestClass : MFFDynamicLinqTypeLocator, 
    IMFFLinqTypeLocatorBaseTestClass
{
    public MFFDynamicLinqTypeLocatorTestClass(IMFFSerializer serializer) : base (serializer) 
    {

    }

    public ImmutableArray<MFFGeneratorInfoWithSrcTypesRecord?> ExposedGetMatchedTypes(
        ImmutableArray<MFFGeneratorInfoRecord?> genInfos, 
        ImmutableArray<MFFTypeSymbolSources> sources, 
        CancellationToken cancellationToken)
    {
        return GetMatchedTypes(genInfos, sources, cancellationToken);
    }
}
