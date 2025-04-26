using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.Serializers;


namespace MavFiFoundation.SourceGenerators.Serializers.UnitTests;

public class MFFXmlSerializersTests
{

    [Fact]
    public void GetTypesWithAttribute_NothingIn_NothingOut ()
    {
        //Arrange
        var xml = 

$$$"""
<{{{nameof(MFFGeneratorInfoModel)}}}>
    <{{{nameof(MFFGeneratorInfoModel.SrcLocatorType)}}}>
        TestLocator
    </{{{nameof(MFFGeneratorInfoModel.SrcLocatorType)}}}>
    <{{{nameof(MFFGeneratorInfoModel.SrcLocatorInfo)}}}>
{
  "Test": "Test"
}
    </{{{nameof(MFFGeneratorInfoModel.SrcLocatorInfo)}}}>
    <{{{nameof(MFFGeneratorInfoModel.GenOutputInfos)}}}>
        <{{{nameof(MFFBuilderModel)}}}>
            <{{{nameof(MFFBuilderModel.FileNameBuilderType)}}}>
                Test Builder
            </{{{nameof(MFFBuilderModel.FileNameBuilderType)}}}>
            <{{{nameof(MFFBuilderModel.FileNameBuilderInfo)}}}>
                AllTestBuilder
            </{{{nameof(MFFBuilderModel.FileNameBuilderInfo)}}}>
            <{{{nameof(MFFBuilderModel.AdditionalOutputInfos)}}}>
                <AdditionalOutputInfo key="AdditionalValueKey">
                    AdditionalOutputValue
                </AdditionalOutputInfo>
            </{{{nameof(MFFBuilderModel.AdditionalOutputInfos)}}}>
        </{{{nameof(MFFBuilderModel)}}}>
    </{{{nameof(MFFGeneratorInfoModel.GenOutputInfos)}}}>
</{{{nameof(MFFGeneratorInfoModel)}}}>
""";

        var cut = new MFFXmlSerializer();

        //Act
        var result = cut.DeserializeObject<MFFGeneratorInfoModel>(xml);

//        result.GenOutputInfos[0].SourceBuilderInfo = new string[]{"Builder Info1","Builder Info2"};/*new Test(){
//            Key = "Info Key",
//            Value = "Info Value"
//        };*/

        if (result is not null)
        {
            var ttt = cut.SerializeObject(result);
        }
 var tttt = Type.GetType("System.String", true);
        //assert
        
    }

    [Fact]
    public void Serialize_NothingIn_NothingOut ()
    {
        //Arrange
        var genInfo = new MFFGeneratorInfoModel();
        genInfo.ContainingNamespace = "Containing.Namespace";
        genInfo.SrcLocatorType = "Source Locator Type";
        genInfo.SrcLocatorInfo = new Test(){
            Key = "Info Key",
            Value = "Info Value"
        };

        //var cut = new MFFXmlSerializer();
        var cut = new MFFJsonSerializer();
        //var cut = new MFFYamlSerializer();

        //Act
        var serialized = cut.SerializeObject(genInfo);
        var actGenInfo = cut.DeserializeObject(serialized, typeof(MFFGeneratorInfoModel));

        //var serializer = new XmlSerializer(genInfo.GetType());
        //using var sw = new StringWriter();
        //using var writer = XmlWriter.Create(sw);
        //serializer.Serialize(writer, genInfo);
        //var xml = sw.ToString();
        //var json = Newtonsoft.Json.JsonConvert.DeserializeObject()).SerializeObject(genInfo);
    }

}
    public class Test
    {
        public string? Key {get;set;}
        public string? Value {get;set;}
    }
