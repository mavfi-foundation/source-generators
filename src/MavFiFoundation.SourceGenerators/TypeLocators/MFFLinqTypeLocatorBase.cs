using Microsoft.CodeAnalysis;
using MavFiFoundation.SourceGenerators.Models;
using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Serializers;

namespace MavFiFoundation.SourceGenerators.TypeLocators;

public abstract class MFFLinqTypeLocatorBase<TLocatorInfo> : MFFGeneratorPluginBase, IMFFTypeLocator
    where TLocatorInfo: MFFTypeLocatorInfoBase
{
    protected IMFFSerializer Serializer { get; private set; }

    public MFFLinqTypeLocatorBase(string name, IMFFSerializer serializer) : base(
        name)
    {
        Serializer = serializer;
    }


    public IncrementalValuesProvider<MFFGeneratorInfoWithSrcTypesRecord?> GetTypeSymbolsProvider(
        IncrementalGeneratorInitializationContext genContext,
        IncrementalValuesProvider<MFFGeneratorInfoRecord?> genInfos,
        IncrementalValuesProvider<MFFTypeSymbolSources> allTypes)
    {
        var pipeline = genInfos.Collect().Combine(allTypes.Collect())
            .SelectMany((combined, cancellationToken) => 
                GetMatchedTypes(combined.Left, combined.Right, cancellationToken));

        return pipeline;

    }

    protected ImmutableArray<MFFGeneratorInfoWithSrcTypesRecord?> GetMatchedTypes(
        ImmutableArray<MFFGeneratorInfoRecord?> genInfos, 
        ImmutableArray<MFFTypeSymbolSources> sources, 
        CancellationToken cancellationToken)
    {
        var genInfoWithSrcsBuilder = ImmutableArray.CreateBuilder<MFFGeneratorInfoWithSrcTypesRecord?>();

        foreach (var genInfo in genInfos)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (genInfo is null) continue;

            var serializedLocatorInfo = (genInfo.SrcLocatorInfo as string)?.Trim();

            TLocatorInfo? locatorInfo = null;

            if (serializedLocatorInfo is not null)
            {
                locatorInfo = DeserializeLocatorInfo(serializedLocatorInfo);
            }

            if (locatorInfo is not null)
            {
                var typeSymbols = ImmutableArray.CreateBuilder<MFFTypeSymbolRecord>();
                string[] sources2Check;

                if (locatorInfo.NoSearchProjectTypes)
                {
                    sources2Check = locatorInfo.Assemblies2Search;
                }
                else
                {
                    sources2Check = [.. locatorInfo.Assemblies2Search,
                            MFFGeneratorConstants.Generator.CompilingProject];
                }

                foreach (var source in sources.Where(s => sources2Check.Contains(s.Source)))
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    IEnumerable<MFFTypeSymbolRecord>? typeQuery = source.Types
                        .Where(GetWherePredicate(locatorInfo, source));

                    if (locatorInfo.Types2Exclude.Any())
                    {
                        typeQuery = typeQuery.Where(t => !locatorInfo
                            .Types2Exclude.Contains(t.FullyQualifiedName));
                    }

                    typeSymbols.AddRange(typeQuery);
                }

                genInfoWithSrcsBuilder.Add(new MFFGeneratorInfoWithSrcTypesRecord(
                    genInfo,
                    typeSymbols.ToImmutable()
                ));
            }
        }

        return genInfoWithSrcsBuilder.ToImmutable();
    }

    protected abstract TLocatorInfo? DeserializeLocatorInfo(string serializedLocatorInfo);

    protected abstract Func<MFFTypeSymbolRecord, bool> GetWherePredicate(
        TLocatorInfo locatorInfo, MFFTypeSymbolSources source);

}