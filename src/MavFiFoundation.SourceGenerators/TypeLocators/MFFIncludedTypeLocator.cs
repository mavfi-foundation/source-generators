using Microsoft.CodeAnalysis;
using MavFiFoundation.SourceGenerators.Models;
using System.Collections.Immutable;

namespace MavFiFoundation.SourceGenerators.TypeLocators;

public class MFFIncludedTypeLocator : MFFGeneratorPluginBase, IMFFTypeLocator
{
    public const string DEFAULT_NAME = nameof(MFFIncludedTypeLocator);

    public MFFIncludedTypeLocator() : base(
        DEFAULT_NAME
    ) { }

    public IncrementalValuesProvider<MFFGeneratorInfoWithSrcTypesRecord?> GetTypeSymbolsProvider(
        IncrementalGeneratorInitializationContext genContext, 
        IncrementalValuesProvider<MFFGeneratorInfoRecord?> genInfos,
        IncrementalValuesProvider<MFFTypeSymbolSources> allTypes)
    {
        var pipeline = genInfos.Select(static (genInfo, cancellationToken) =>GetIncludedType(genInfo)).
            Where(static _ => _ is not null);

        return pipeline;
    }

    protected static MFFGeneratorInfoWithSrcTypesRecord? GetIncludedType(MFFGeneratorInfoRecord? genInfo)
    {
        if (genInfo is not null && genInfo.SrcLocatorInfo is MFFTypeSymbolRecord)
        {
            var typeSymbol = genInfo.SrcLocatorInfo as MFFTypeSymbolRecord;

            if (typeSymbol is not null)
            {
                MFFTypeSymbolRecord[] srcTypes = [typeSymbol];
                return new MFFGeneratorInfoWithSrcTypesRecord(genInfo, srcTypes.ToImmutableArray());
            }
        }

        return null;
    }
}
