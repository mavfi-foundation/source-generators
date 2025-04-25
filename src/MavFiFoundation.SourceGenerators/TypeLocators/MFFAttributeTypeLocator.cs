using Microsoft.CodeAnalysis;
using MavFiFoundation.SourceGenerators.Models;
using System.Collections.Immutable;
using Newtonsoft.Json;

namespace MavFiFoundation.SourceGenerators.TypeLocators;

public class MFFAttributeTypeLocator : MFFGeneratorPluginBase, IMFFTypeLocator
{
    public const string DEFAULT_NAME = nameof(MFFAttributeTypeLocator);

    public MFFAttributeTypeLocator() : base(
        DEFAULT_NAME) { }

    public IncrementalValuesProvider<MFFGeneratorInfoWithSrcTypesRecord?> GetTypeSymbolsProvider(
        IncrementalGeneratorInitializationContext genContext, 
        IncrementalValuesProvider<MFFGeneratorInfoRecord?> genInfos,
        IncrementalValuesProvider<MFFTypeSymbolSources> allTypes)
    {
        var pipeline = genInfos.Collect().Combine(allTypes.Collect())
            .SelectMany((combined, cancellationToken) =>
        {
            var genInfoWithSrcsBuilder = ImmutableArray.CreateBuilder<MFFGeneratorInfoWithSrcTypesRecord?>();

            foreach (var genInfo in combined.Left)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if(genInfo is null) continue;

                //TODO: MFFTypeNameTypeLocator.cs string[] of type names, string[] Assemblies2Search - wildcard/regex find
                //TODO: dynamic linq where typelocator

				var serializedLocatorInfo = genInfo.SrcLocatorInfo as string;

                MFFAttributeTypeLocatorInfo? locatorInfo = null;

				if (serializedLocatorInfo is not null)
				{
                    if (serializedLocatorInfo.TrimStart().StartsWith("{"))
                    {
    					locatorInfo = JsonConvert.DeserializeObject<MFFAttributeTypeLocatorInfo?>(serializedLocatorInfo);
                    }
                    else
                    {
                        locatorInfo = new MFFAttributeTypeLocatorInfo()
                        {
                            Attribute2Find = serializedLocatorInfo
                        };
                    }
				}

                if (locatorInfo is not null && !string.IsNullOrWhiteSpace(locatorInfo.Attribute2Find))
                {
                    var typeSymbols = ImmutableArray.CreateBuilder<MFFTypeSymbolRecord>();
                    string[] sources2Check;

                    if(locatorInfo.NoSearchProjectTypes)
                    {
                        sources2Check = locatorInfo.Assemblies2Search;
                    }
                    else
                    {
                        sources2Check = [.. locatorInfo.Assemblies2Search, 
                            MFFGeneratorConstants.Generator.COMPILING_PROJECT];
                    }

                    foreach (var source in combined.Right.Where(s=>sources2Check.Contains(s.Source)))
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        
                        var typeQuery = source.Types
                            .Where(t=>t.Attributes.Any(a=>a.Name == locatorInfo.Attribute2Find));

                        if(locatorInfo.Types2Exclude.Any())
                        {
                            typeQuery = typeQuery.Where(t=> locatorInfo
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
        });

        return pipeline;
 
    }
}