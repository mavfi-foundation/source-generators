using MavFiFoundation.SourceGenerators;
using MavFiFoundation.SourceGenerators.Builders;
using MavFiFoundation.SourceGenerators.TypeLocators;

namespace TestSpace
{
	[MFFGenerateSource(MFFAttributeTypeLocator.DefaultName,
	"TestSpace.MFFAttributeGeneratorTrigger_AttributeTypeLocator_ScribanBuilder_GeneratesClass_AttributeAttribute",
"""
[
	{
		"FileNameBuilderInfo": "{{ srcType.Name }}_Generated.g.cs", 
""" +
$"\n		\"SourceBuilderType\": \"{ MFFScribanBuilder.DefaultName }\",\n" +
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
    public class MFFAttributeGeneratorTrigger_AttributeTypeLocator_ScribanBuilder_GeneratesClass_Generate
    {

    }
}