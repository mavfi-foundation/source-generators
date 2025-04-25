using MavFiFoundation.SourceGenerators;
using MavFiFoundation.SourceGenerators.Builders;
using MavFiFoundation.SourceGenerators.TypeLocators;
using MavFiFoundation.SourceGenerators.ResourceLoaders;
using MavFiFoundation.SourceGenerators.TestSupport;

namespace TestSpace
{
	[MFFGenerateSource(MFFIncludedTypeLocator.DEFAULT_NAME,
"""
[
	{
		"FileNameBuilderInfo": "{{ srcType.Name }}_Generated.g.cs", 
""" +
$"\n		\"SourceBuilderType\": \"{ MFFScribanBuilder.DEFAULT_NAME }\",\n" +
$"  		\"SourceBuilderInfo\": \"{ MFFResourceLoader.DEFAULT_LOADER_PREFIX + Constants.SourceFiles.Code.TEST_EMBEDDED_RESOURCE_CLASS}\" \n" +
"""
	}
]
"""
	)]
    public class ResourceLoader_LoadsEmbeddedResources
    {

    }
}
