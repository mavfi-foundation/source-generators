using MavFiFoundation.SourceGenerators;
using MavFiFoundation.SourceGenerators.Builders;
using MavFiFoundation.SourceGenerators.TypeLocators;
using MavFiFoundation.SourceGenerators.ResourceLoaders;
using MavFiFoundation.SourceGenerators.TestSupport;

namespace TestSpace
{
	[MFFGenerateSource(MFFIncludedTypeLocator.DefaultName, null,
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
        private readonly bool _testField = false;

        public bool TestProperty { get; set; }

        protected bool TestMethod(bool testParameter1, string testParameter2)
        {
            return _testField;
        }
    }
}
