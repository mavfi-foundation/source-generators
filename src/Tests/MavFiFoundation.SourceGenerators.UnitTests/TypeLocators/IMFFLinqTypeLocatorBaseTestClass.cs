using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.UnitTests.TypeLocators;

public interface IMFFLinqTypeLocatorBaseTestClass
{
    public ImmutableArray<MFFGeneratorInfoWithSrcTypesRecord?> ExposedGetMatchedTypes(
        ImmutableArray<MFFGeneratorInfoRecord?> genInfos, 
        ImmutableArray<MFFTypeSymbolSources> sources, 
        CancellationToken cancellationToken);
}
