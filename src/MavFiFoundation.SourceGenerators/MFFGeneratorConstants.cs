
namespace MavFiFoundation.SourceGenerators;

public static class MFFGeneratorConstants
{
    public static class Generator 
    {
        public const string COMPILING_PROJECT = "Self";

        public const string EMBEDDED_RESOURCE_ATTRIBUTE_NAME = 
            "MavFiFoundation.SourceGenerators.MFFEmbeddedResourceAttribute";
        public const string CREATE_GENERATOR_CONSTANTS_ATTRIBUTE_NAME = 
            "MavFiFoundation.SourceGenerators.MFFCreateGeneratorConstantsAttribute";

        public const string CREATE_GENERATOR_CONSTANTS_OUTPUT_NAME = 
            "MFFGeneratorConstants.g.cs";

        public const string CREATE_GENERATOR_CONSTANTS_TEMPLATE_NAME = 
            "MFFGeneratorConstants.scriban-cs";

    }
/*
    public static class Builder
    {
        public const string LIQUID_BUILDER_DEFAULT_NAME = nameof(MFFLiquidBuilder);
        
        public const string SCRIBAN_BUILDER_DEFAULT_NAME = nameof(MFFScribanBuilder);

    }

    public static class GeneratorTrigger
    {
        public const string ATTRIBUTE_CONFIG_LOCATOR_DEFAULT_NAME = nameof(MFFAttributeGeneratorTrigger);

	    public const string ATTRIBUTE_CONFIG_LOCATOR_DEFAULT_ATTRIBUTE_NAME = "MavFiFoundation.SourceGenerators.MFFGenerateSourceAttribute";

        public const string JSON_CONFIG_LOCATOR_DEFAULT_NAME = nameof(MFFJsonGeneratorTrigger);
        public const string XML_CONFIG_LOCATOR_DEFAULT_NAME = nameof(MFFXmlGeneratorTrigger);
    }

    public static class ResourceLoader
    {
        public const string RESOURCE_LOADER_DEFAULT_NAME = nameof(MFFResourceLoader);

        public const string RESOURCE_LOADER_PREFIX = nameof(MFFResourceLoader) + ":";
    }

    public static class TypeLocator
    {
        public const string ATTRIBUTE_TYPE_LOCATOR_DEFAULT_NAME = nameof(MFFAttributeTypeLocator);

        public const string INCLUDED_TYPE_LOCATOR_DEFAULT_NAME = nameof(MFFIncludedTypeLocator);
    }
*/

}
