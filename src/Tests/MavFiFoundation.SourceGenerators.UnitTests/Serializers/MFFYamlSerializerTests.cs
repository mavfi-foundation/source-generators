
namespace MavFiFoundation.SourceGenerators.Serializers.UnitTests;

public class MFFYamlSerializerTests : MFFSerializerTestsBase
{
    [Fact]
    public void MFFYamlSerializer_Serialize_Deserialize_All_MFFGeneratorInfoModel_Properties ()
    {
        Serialize_Deserialize_All_MFFGeneratorInfoModel_Properties(new MFFYamlSerializer());
    }
}