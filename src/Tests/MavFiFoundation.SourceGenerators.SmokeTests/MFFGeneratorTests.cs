namespace MavFiFoundation.SourceGenerators.SmokeTests;

public class MFFGeneratorTests
{
    [Theory]
    [InlineData("GeneratedByIncludedTypeTest")]
    [InlineData("GeneratedByJsonAttributeTest")]
    [InlineData("GeneratedByXmlAttributeTest")]
    [InlineData("GeneratedByYamlAttributeTest")]
    public void Types_Are_Generated(string typeName)
    {
        var expected = Type.GetType(typeName);
        expected.Should().NotBeNull();
    }
}

/*
	[MFFGenerateSource(GeneratorConstants.TypeLocator.MFFIncludedTypeLocator,
"TestSpace.MFFAttributeGeneratorTrigger_AttributeTypeLocator_ScribanBuilder_GeneratesClass_AttributeAttribute",
"""
[
]
"""
)]
    public class MFFAttributeGeneratorTrigger_NoOutputs
    {

    }

	[MFFGenerateSource("InvalidTypeLocator",
	"TestSpace.MFFAttributeGeneratorTrigger_AttributeTypeLocator_ScribanBuilder_GeneratesClass_AttributeAttribute",
"""
[
	{
		"FileNameBuilderInfo": "{{ srcType.Name }}_Generated.g.cs", 
""" +
$"\n		\"SourceBuilderType\": \"{ GeneratorConstants.Builder.MFFScribanBuilder }\",\n" +
"""
		"SourceBuilderInfo": "#nullable enable\n\n
""" +
"""
public partial class {{ srcType.Name }}_Generated\n{\n\n}"
""" + 
"""
	}
]
"""
	)]
    public class MFFAttributeGeneratorTrigger_InvalidTypeLocator
    {

    }
*/