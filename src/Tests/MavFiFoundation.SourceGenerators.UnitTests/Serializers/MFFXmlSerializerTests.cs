
namespace MavFiFoundation.SourceGenerators.Serializers.UnitTests;

public class MFFXmlSerializerTests : MFFSerializerTestsBase
{
    [Fact]
    public void MFFXmlSerializer_Serialize_Deserialize_All_MFFGeneratorInfoModel_Properties ()
    {
        Serialize_Deserialize_All_MFFGeneratorInfoModel_Properties(new MFFXmlSerializer(), true);
    }
}