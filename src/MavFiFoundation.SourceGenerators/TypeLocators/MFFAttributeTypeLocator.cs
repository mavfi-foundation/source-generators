// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.Serializers;
using System.Text.RegularExpressions;

namespace MavFiFoundation.SourceGenerators.TypeLocators;

/// <summary>
/// Provides functionality to locate types based on attribute names using a LINQ-based approach.
/// </summary>
/// <remarks>
/// This type locator is designed to identify types that are decorated with a specific attribute,
/// either by matching a fully qualified attribute name or by deserializing locator information.
/// </remarks>
public class MFFAttributeTypeLocator : MFFLinqTypeLocatorBase<MFFAttributeTypeLocatorInfo>
{
    /// <summary>
    /// Default name used to identify the type locator plugin.
    /// </summary>
    public const string DefaultName = nameof(MFFAttributeTypeLocator);

    /// <summary>
    /// Regular expression used to determine a string is a fully qualified name.
    /// </summary>
    protected Regex FullyQualifiedNameRegex { get; private set; }

    public MFFAttributeTypeLocator(IMFFSerializer serializer) : base(
        DefaultName, serializer)
    {
        FullyQualifiedNameRegex = new Regex(@"^[A-Za-z_][A-Za-z0-9_]*(\.[A-Za-z_][A-Za-z0-9_]*)*$",
            RegexOptions.Compiled);
    }

    /// <inheritdoc/>
    protected override MFFAttributeTypeLocatorInfo? DeserializeLocatorInfo(string serializedLocatorInfo)
    {
        MFFAttributeTypeLocatorInfo? locatorInfo = null;

        if (FullyQualifiedNameRegex.IsMatch(serializedLocatorInfo))
        {
            locatorInfo = new MFFAttributeTypeLocatorInfo()
            {
                Attribute2Find = serializedLocatorInfo
            };
        }
        else
        {
            locatorInfo = Serializer.DeserializeObject<MFFAttributeTypeLocatorInfo>(serializedLocatorInfo);
        }

        return locatorInfo;

    }

    /// <inheritdoc/>
    protected override Func<MFFTypeSymbolRecord, bool> GetWherePredicate(
        MFFAttributeTypeLocatorInfo locatorInfo, MFFTypeSymbolSources source) =>
            t => t.Attributes.Any(a => a.Name == locatorInfo.Attribute2Find);
}