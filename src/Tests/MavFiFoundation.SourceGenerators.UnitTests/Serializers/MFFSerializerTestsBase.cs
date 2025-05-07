using MavFiFoundation.SourceGenerators.Models;

namespace MavFiFoundation.SourceGenerators.Serializers.UnitTests;

public abstract class MFFSerializerTestsBase
{

    protected MFFGeneratorInfoModel MFFGeneratorInfoModel_Fully_Populated
    {
        get
        {
            return new MFFGeneratorInfoModel()
            {
                ContainingNamespace = "TestSpace",
                SrcLocatorType = "MFFAttributeTypeLocator",
                SrcLocatorInfo = "TestSpace.MFFYamlGeneratorTrigger_AttributeTypeLocator_LiquidBuilder_GeneratesClass_AttributeAttribute",
                GenOutputInfos= new List<MFFBuilderModel>()
                {
                    new MFFBuilderModel()  
                    {
                        FileNameBuilderInfo = "Generator_Generated.g.cs",
                        FileNameBuilderType = "MFFLiquidBuilder",
                        SourceBuilderType = "MFFScribanBuilder",
                        AdditionalOutputInfos = new Dictionary<string, object>()
                        {
                            { "GenTestKey", "GenTestValue" }
                        },
                        SourceBuilderInfo =
"""
#nullable enable

public partial class Generator_Generated
{

}

"""
                    }
                },
                SrcOutputInfos = new List<MFFBuilderModel>()
                {
                    new MFFBuilderModel()  
                    {
                        FileNameBuilderInfo = "{{ srcType.Name }}_Generated.g.cs",
                        FileNameBuilderType = "MFFScribanBuilder",
                        SourceBuilderType = "MFFLiquidBuilder",
                        AdditionalOutputInfos = new Dictionary<string, object>()
                        {
                            { "ScrTestKey", "ScrTestValue" }
                        },
                        SourceBuilderInfo =
"""
#nullable enable
public partial class {{ srcType.Name }}_Generated
{

}

"""
                    }
                }
            };
        }
    }

    protected void Serialize_Deserialize_All_MFFGeneratorInfoModel_Properties (IMFFSerializer cut)
    {
        //Arrange
        var genInfo = MFFGeneratorInfoModel_Fully_Populated;

        //Act
        var serialized = cut.SerializeObject(genInfo);
        var actGenInfo = cut.DeserializeObject(serialized, typeof(MFFGeneratorInfoModel));

        actGenInfo.Should().BeEquivalentTo(genInfo);
    }

}