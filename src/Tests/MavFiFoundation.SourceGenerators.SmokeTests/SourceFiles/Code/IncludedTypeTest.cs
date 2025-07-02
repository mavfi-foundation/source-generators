using MavFiFoundation.SourceGenerators;

namespace TestSpace
{
	[MFFGenerateSource(GeneratorConstants.TypeLocator.MFFIncludedTypeLocator, null,
"""
[
	{
		"FileNameBuilderInfo": "GeneratedBy{{ srcType.Name }}.g.cs", 
""" +
$"\n		\"SourceBuilderType\": \"{GeneratorConstants.Builder.MFFScribanBuilder }\",\n" +
"""
		"SourceBuilderInfo": "#nullable enable\n\n
""" +
"""
public partial class GeneratedBy{{ srcType.Name }} { }"
""" + 
"""
	}
]
"""
	)]
	public class IncludedTypeTest
	{ 

	}
}