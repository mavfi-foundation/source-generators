using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.Serializers;

namespace MavFiFoundation.SourceGenerators.TypeLocators;

public class MFFDynamicLinqTypeLocator : MFFLinqTypeLocatorBase<MFFDynamicLinqTypeLocatorInfo>
{
    public const string DEFAULT_NAME = nameof(MFFDynamicLinqTypeLocator);

    public MFFDynamicLinqTypeLocator(IMFFSerializer serializer) : base(
        DEFAULT_NAME, serializer)
    {

    }

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

    protected override Func<MFFTypeSymbolRecord, bool> GetWherePredicate(
        MFFDynamicLinqTypeLocatorInfo locatorInfo, MFFTypeSymbolSources source)
    {

        LambdaExpression lambda = DynamicExpressionParser.ParseLambda(
            typeof(MFFTypeSymbolRecord), typeof(bool), locatorInfo.LinqWhere, source);
        
        return lambda.Compile() as Func<MFFTypeSymbolRecord, bool> ??
            throw new InvalidCastException("lambda");
    }
}