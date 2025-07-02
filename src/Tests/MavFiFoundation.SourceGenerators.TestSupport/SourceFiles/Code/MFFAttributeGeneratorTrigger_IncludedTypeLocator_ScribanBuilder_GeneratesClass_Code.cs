using MavFiFoundation.SourceGenerators;
using MavFiFoundation.SourceGenerators.Builders;
using MavFiFoundation.SourceGenerators.TypeLocators;


namespace TestSpace;

	[MFFGenerateSource(MFFIncludedTypeLocator.DefaultName, null,
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
""",
"""
[
	{
		"FileNameBuilderInfo": "{{ srcTypes[0].Name }}_Generated2.g.cs", 
""" +
$"\n		\"SourceBuilderType\": \"{ MFFScribanBuilder.DefaultName }\",\n" +
"""
		"SourceBuilderInfo": "#nullable enable\n\n
""" +
"""
public partial class {{ srcTypes[0].Name }}_Generated2\n{\n\n}"
""" + 
"""
	}
]
"""
	)]
public class MFFAttributeGeneratorTrigger_IncludedTypeLocator_ScribanBuilder_GeneratesClass_Code
{

}
