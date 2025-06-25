// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using System.Collections.Immutable;

namespace MavFiFoundation.SourceGenerators.Models;

/// <summary>
/// Provides extension methods for converting between model and record types used in the MavFi Foundation source generators,
/// as well as utility methods for filtering accessible properties and fields.
/// </summary>
public static class ModelExtensions
{
    /// <summary>
    /// Converts an <see cref="MFFBuilderRecord"/> instance to an <see cref="MFFBuilderModel"/> instance.
    /// </summary>
    /// <param name="self">The <see cref="MFFBuilderRecord"/> to convert.</param>
    /// <returns>A new <see cref="MFFBuilderModel"/> instance with values copied from the record.</returns>
    public static MFFBuilderModel ToModel(this MFFBuilderRecord self)
    {
        var model = new MFFBuilderModel();

        model.FileNameBuilderType = self.FileNameBuilderType;
        model.FileNameBuilderInfo = self.FileNameBuilderInfo;
        model.SourceBuilderType = self.SourceBuilderType;
        model.SourceBuilderInfo = self.SourceBuilderInfo;
        if (!self.AdditionalOutputInfos.IsDefaultOrEmpty)
        {
            model.AdditionalOutputInfos = self.AdditionalOutputInfos
                .ToDictionary(a => a.Key, a => a.Value);
        }

        return model;
    }

    /// <summary>
    /// Converts an <see cref="MFFBuilderModel"/> instance to an <see cref="MFFBuilderRecord"/> instance.
    /// </summary>
    /// <param name="self">The <see cref="MFFBuilderModel"/> to convert.</param>
    /// <returns>A new <see cref="MFFBuilderRecord"/> instance with values copied from the model.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="self"/> contains null values for required properties.
    /// </exception>
    public static MFFBuilderRecord ToRecord(this MFFBuilderModel self)
    {
        EquatableArray<(string Key, object Value)> additionalOutputInfos = default;

        if (self.AdditionalOutputInfos is not null)
        {
            additionalOutputInfos = self.AdditionalOutputInfos.Select(a => (Key: a.Key, Value: a.Value)).ToImmutableArray();
        }

        return new MFFBuilderRecord(
            self.FileNameBuilderType,
            self.FileNameBuilderInfo ??
                throw new ArgumentNullException(nameof(self.FileNameBuilderInfo)),
            self.SourceBuilderType ??
                throw new ArgumentNullException(nameof(self.SourceBuilderType)),
            self.SourceBuilderInfo ??
                throw new ArgumentNullException(nameof(self.SourceBuilderInfo)),
            additionalOutputInfos
        );
    }

    /// <summary>
    /// Converts an <see cref="MFFGeneratorInfoRecord"/> instance to an <see cref="MFFGeneratorInfoModel"/> instance.
    /// </summary>
    /// <param name="self">The <see cref="MFFGeneratorInfoRecord"/> to convert.</param>
    /// <returns>A new <see cref="MFFGeneratorInfoModel"/> instance with values copied from the record.</returns>
    public static MFFGeneratorInfoModel ToModel(this MFFGeneratorInfoRecord self)
    {
        var model = new MFFGeneratorInfoModel();

        model.ContainingNamespace = self.ContainingNamespace;
        model.SrcLocatorType = self.SrcLocatorType;
        model.SrcLocatorInfo = self.SrcLocatorInfo;
        model.GenOutputInfos = self.GenOutputInfos.Select(or => or.ToModel()).ToList();
        model.SrcOutputInfos = self.SrcOutputInfos.Select(or => or.ToModel()).ToList();

        return model;
    }

    /// <summary>
    /// Converts an <see cref="MFFGeneratorInfoModel"/> instance to an <see cref="MFFGeneratorInfoRecord"/> instance.
    /// </summary>
    /// <param name="self">The <see cref="MFFGeneratorInfoModel"/> to convert.</param>
    /// <returns>A new <see cref="MFFGeneratorInfoRecord"/> instance with values copied from the model.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="self"/> contains null values for required properties.
    /// </exception>
    public static MFFGeneratorInfoRecord ToRecord(this MFFGeneratorInfoModel self)
    {
        EquatableArray<MFFBuilderRecord> genOutputInfos = default;

        if (self.GenOutputInfos is not null)
        {
            genOutputInfos = self.GenOutputInfos.Select(br => br.ToRecord()).ToImmutableArray();
        }

        EquatableArray<MFFBuilderRecord> srcOutputInfos = default;

        if (self.SrcOutputInfos is not null)
        {
            srcOutputInfos = self.SrcOutputInfos.Select(br => br.ToRecord()).ToImmutableArray();
        }

        return new MFFGeneratorInfoRecord(
            self.ContainingNamespace ??
                throw new ArgumentNullException(nameof(self.ContainingNamespace)),
            self.SrcLocatorType ??
                throw new ArgumentNullException(nameof(self.SrcLocatorType)),
            self.SrcLocatorInfo ??
                throw new ArgumentNullException(nameof(self.SrcLocatorInfo)),
            genOutputInfos,
            srcOutputInfos
        );
    }

    /// <summary>
    /// Filters a collection of <see cref="MFFTypePropertyRecord"/> to return only those that are publicly accessible.
    /// </summary>
    /// <param name="properties">The collection of properties to filter.</param>
    /// <param name="includeIfSetIsPublic">Whether to include properties with a public set method.</param>
    /// <param name="includeIfGetIsPublic">Whether to include properties with a public get method.</param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of <see cref="MFFTypePropertyRecord"/> representing accessible properties.
    /// </returns>
    public static IEnumerable<MFFTypePropertyRecord> AccessibleProperties(
        IEnumerable<MFFTypePropertyRecord> properties,
        bool includeIfSetIsPublic = false,
        bool includeIfGetIsPublic = true)
    {
        return properties.Where(p => p.DeclaredAccessibilty == MFFAccessibilityType.Public &&
            ((includeIfGetIsPublic && p.GetMethod is not null &&
                p.GetMethod.DeclaredAccessibilty == MFFAccessibilityType.Public) ||
            (includeIfSetIsPublic && p.SetMethod is not null &&
                p.SetMethod.DeclaredAccessibilty == MFFAccessibilityType.Public)));
    }

    /// <summary>
    /// Filters a collection of <see cref="MFFTypeFieldRecord"/> to return only those that are publicly accessible.
    /// </summary>
    /// <param name="fields">The collection of fields to filter.</param>
    /// <returns>
    /// An <see cref="IEnumerable{T}"/> of <see cref="MFFTypeFieldRecord"/> representing accessible fields.
    /// </returns>
    public static IEnumerable<MFFTypeFieldRecord> AccessibleFields(
        IEnumerable<MFFTypeFieldRecord> fields)
    {
        return fields.Where(p => p.DeclaredAccessibilty == MFFAccessibilityType.Public);
    }

}
