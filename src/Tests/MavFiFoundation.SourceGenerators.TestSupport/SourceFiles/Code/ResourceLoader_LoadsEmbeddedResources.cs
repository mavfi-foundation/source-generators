using MavFiFoundation.SourceGenerators;
using MavFiFoundation.SourceGenerators.Builders;
using MavFiFoundation.SourceGenerators.TypeLocators;
using MavFiFoundation.SourceGenerators.ResourceLoaders;
using MavFiFoundation.SourceGenerators.TestSupport;

namespace TestSpace
{
	[MFFGenerateSource(MFFIncludedTypeLocator.DefaultName,
"""
[
	{
		"FileNameBuilderInfo": "{{ srcType.Name }}_Generated.g.cs", 
""" +
$"\n		\"SourceBuilderType\": \"{ MFFScribanBuilder.DefaultName }\",\n" +
$"  		\"SourceBuilderInfo\": \"{ MFFResourceLoader.DefaultLoaderPrefix + Constants.SourceFiles.Code.TestEmbeddedResourceClass}\" \n" +
"""
	}
]
"""
	)]
    public class ResourceLoader_LoadsEmbeddedResources
    {

    }
}
