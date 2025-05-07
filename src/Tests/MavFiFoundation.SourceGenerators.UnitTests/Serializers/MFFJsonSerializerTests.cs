
namespace MavFiFoundation.SourceGenerators.Serializers.UnitTests;

public class MFFJsonSerializerTests : MFFSerializerTestsBase
{
    [Fact]
    public void MFFJsonSerializer_Serialize_Deserialize_All_MFFGeneratorInfoModel_Properties ()
    {
        Serialize_Deserialize_All_MFFGeneratorInfoModel_Properties(new MFFJsonSerializer());
    }
}