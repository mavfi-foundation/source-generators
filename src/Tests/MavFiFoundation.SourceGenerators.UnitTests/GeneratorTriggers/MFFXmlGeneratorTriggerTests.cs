using MavFiFoundation.SourceGenerators.GeneratorTriggers;
using MavFiFoundation.SourceGenerators.Serializers;
using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.UnitTests.GeneratorTriggers;

public class MFFXmlGeneratorTriggerTests : MFFXmlGeneratorTrigger
{

    public MFFXmlGeneratorTriggerTests() : base (new MFFXmlSerializer()) { }

    [Fact]
    public void GetTypesWithAttribute_NothingIn_NothingOut ()
    {
        var xml = 

$$"""
<{{nameof(MFFGeneratorInfoModel)}}>
    <{{nameof(MFFGeneratorInfoModel.SrcLocatorType)}}>
        TestLocator
    </{{nameof(MFFGeneratorInfoModel.SrcLocatorType)}}>
    <{{nameof(MFFGeneratorInfoModel.SrcLocatorInfo)}}>
{
  "Test": "Test"
}
    </{{nameof(MFFGeneratorInfoModel.SrcLocatorInfo)}}>
    <{{nameof(MFFGeneratorInfoModel.GenOutputInfos)}}>
        <{{nameof(MFFBuilderModel.FileNameBuilderType)}}>
            Test Builder
        </{{nameof(MFFBuilderModel.FileNameBuilderType)}}>
        <{{nameof(MFFBuilderModel.AdditionalOutputInfos)}} Key="AdditionalValueKey">
            AdditionalOutputValue
        </{{nameof(MFFBuilderModel.AdditionalOutputInfos)}}>
    </{{nameof(MFFGeneratorInfoModel.GenOutputInfos)}}>
</{{nameof(MFFGeneratorInfoModel)}}>

""";

        var result = Deserialize(xml);

        if (result is not null)
        {
            var ttt = Serializer.SerializeObject(result);
        }
        
    }

}
