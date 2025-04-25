using MavFiFoundation.SourceGenerators;

namespace TestSpace
{
	[MFFGenerateSource(GeneratorConstants.TypeLocator.MFFIncludedTypeLocator,
"""
[
	{
		"FileNameBuilderInfo": "{{ srcType.Name }}BuilderInfo.g.cs", 
""" +
$"\n		\"SourceBuilderType\": \"{GeneratorConstants.Builder.MFFScribanBuilder }\",\n" +
"""
		"SourceBuilderInfo": "#nullable enable\n\n
""" +
"""
public partial class {{ srcType.Name }}BuilderInfo { }"
""" + 
"""
	}
]
"""
	)]
	public class Test
	{ 

	}
}