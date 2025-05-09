using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.Serializers;
using System.Text.RegularExpressions;

namespace MavFiFoundation.SourceGenerators.TypeLocators;

public class MFFAttributeTypeLocator : MFFLinqTypeLocatorBase<MFFAttributeTypeLocatorInfo>
{
    public const string DEFAULT_NAME = nameof(MFFAttributeTypeLocator);

    protected Regex FullyQualifiedNameRegex {get; private set;}

    public MFFAttributeTypeLocator(IMFFSerializer serializer) : base(
        DEFAULT_NAME, serializer)
    {
        FullyQualifiedNameRegex = new Regex(@"^[A-Za-z_][A-Za-z0-9_]*(\.[A-Za-z_][A-Za-z0-9_]*)*$",
            RegexOptions.Compiled);
    }

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

    protected override Func<MFFTypeSymbolRecord, bool> GetWherePredicate(
        MFFAttributeTypeLocatorInfo locatorInfo, MFFTypeSymbolSources source) =>
            t => t.Attributes.Any(a => a.Name == locatorInfo.Attribute2Find);
}