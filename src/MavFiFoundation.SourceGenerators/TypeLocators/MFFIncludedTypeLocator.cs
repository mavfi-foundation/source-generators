// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using Microsoft.CodeAnalysis;
using MavFiFoundation.SourceGenerators.Models;
using System.Collections.Immutable;

namespace MavFiFoundation.SourceGenerators.TypeLocators;

/// <summary>
/// Provides a type locator implementation that will use the type that was included by the generator trigger.
/// </summary>
public class MFFIncludedTypeLocator : MFFGeneratorPluginBase, IMFFTypeLocator
{
    /// <summary>
    /// Default name used to identify the type locator plugin.
    /// </summary>
    public const string DefaultName = nameof(MFFIncludedTypeLocator);

    public MFFIncludedTypeLocator() : base(
        DefaultName
    ) { }

   /// <inheritdoc/>
    public IncrementalValuesProvider<MFFGeneratorInfoWithSrcTypesRecord?> GetTypeSymbolsProvider(
        IncrementalGeneratorInitializationContext genContext, 
        IncrementalValuesProvider<MFFGeneratorInfoRecord?> genInfos,
        IncrementalValuesProvider<MFFTypeSymbolSources> allTypes)
    {
        var pipeline = genInfos.Select(static (genInfo, cancellationToken) =>GetIncludedType(genInfo)).
            Where(static _ => _ is not null);

        return pipeline;
    }

   /// <inheritdoc/>
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
