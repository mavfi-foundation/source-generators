using System.Collections.Immutable;

namespace MavFiFoundation.SourceGenerators.Models;

public static class ModelExtensions
{
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

    private static IEnumerable<MFFTypePropertyRecord> AccessibleProperties(
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

    private static IEnumerable<MFFTypeFieldRecord> AccessibleFields(
        IEnumerable<MFFTypeFieldRecord> fields)
    {
        return fields.Where(p => p.DeclaredAccessibilty == MFFAccessibilityType.Public);
    }

}
