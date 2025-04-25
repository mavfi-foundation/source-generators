using MavFiFoundation.SourceGenerators;
using MavFiFoundation.SourceGenerators.Builders;
using MavFiFoundation.SourceGenerators.TypeLocators;


namespace TestSpace;

	[MFFGenerateSource(MFFIncludedTypeLocator.DEFAULT_NAME,
"""
[
	{
		"FileNameBuilderInfo": "{{ srcType.Name }}_Generated.g.cs", 
""" +
$"\n		\"SourceBuilderType\": \"{ MFFScribanBuilder.DEFAULT_NAME }\",\n" +
"""
		"SourceBuilderInfo": "#nullable enable\n\n
""" +
"""
public partial class {{ srcType.Name }}_Generated
{

}"
""" + 
"""
	}
]
"""
	)]
public class MFFAttributeGeneratorTrigger_IncludedTypeLocator_ScribanBuilder_GeneratesClass_Code
{

}
