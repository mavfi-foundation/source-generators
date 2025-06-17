using MavFiFoundation.SourceGenerators;
using MavFiFoundation.SourceGenerators.Builders;
using MavFiFoundation.SourceGenerators.TypeLocators;


namespace TestSpace;

	[MFFGenerateSource(MFFIncludedTypeLocator.DefaultName,
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
public class MFFAttributeGeneratorTrigger_IncludedTypeLocator_ScribanBuilder_GeneratesClass_Code
{

}
