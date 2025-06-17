// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.Serializers;

namespace MavFiFoundation.SourceGenerators.TypeLocators;

/// <summary>
/// Provides a type locator implementation that uses dynamic LINQ expressions to filter <see cref="MFFTypeSymbolRecord"/> instances.
/// </summary>
/// <remarks>
/// This locator parses and compiles LINQ expressions at runtime, allowing flexible and dynamic type filtering.
/// </remarks>
public class MFFDynamicLinqTypeLocator : MFFLinqTypeLocatorBase<MFFDynamicLinqTypeLocatorInfo>
{
    /// <summary>
    /// Default name used to identify the type locator plugin.
    /// </summary>
    public const string DefaultName = nameof(MFFDynamicLinqTypeLocator);

    public MFFDynamicLinqTypeLocator(IMFFSerializer serializer) : base(
        DefaultName, serializer)
    {

    }

   /// <inheritdoc/>
   protected override MFFDynamicLinqTypeLocatorInfo? DeserializeLocatorInfo(string serializedLocatorInfo)
    {
        MFFDynamicLinqTypeLocatorInfo? locatorInfo = null;
 
        if (!serializedLocatorInfo.ToLower().Contains("linqwhere"))
        {
            locatorInfo = new MFFDynamicLinqTypeLocatorInfo()
            {
                LinqWhere = serializedLocatorInfo
            };
        }
        else
        {
            locatorInfo = Serializer.DeserializeObject<MFFDynamicLinqTypeLocatorInfo>(
                serializedLocatorInfo);
        }

        return locatorInfo;

    }

    /// <inheritdoc/>
    protected override Func<MFFTypeSymbolRecord, bool> GetWherePredicate(
        MFFDynamicLinqTypeLocatorInfo locatorInfo, MFFTypeSymbolSources source)
    {

        LambdaExpression lambda = DynamicExpressionParser.ParseLambda(
            typeof(MFFTypeSymbolRecord), typeof(bool), locatorInfo.LinqWhere, source);

        return lambda.Compile() as Func<MFFTypeSymbolRecord, bool> ??
            throw new InvalidCastException("lambda");
    }
}