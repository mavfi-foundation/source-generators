using System;

namespace MavFiFoundation.SourceGenerators;
public static class GeneratorConstants
{

    public static class Builder
    {

        public const string MFFScribanBuilder = "MFFScribanBuilder";

        public const string MFFLiquidBuilder = "MFFLiquidBuilder";
    }

    public static class GeneratorTrigger
    {

        public const string MFFAttributeGeneratorTrigger = "MFFAttributeGeneratorTrigger";

        public const string MFFXmlGeneratorTrigger = "MFFXmlGeneratorTrigger";

        public const string MFFJsonGeneratorTrigger = "MFFJsonGeneratorTrigger";

        public const string MFFYamlGeneratorTrigger = "MFFYamlGeneratorTrigger";
    }

    public static class ResourceLoader
    {

        public const string MFFResourceLoader = "MFFResourceLoader";
    }
 
    public static class TypeLocator
    {

        public const string MFFIncludedTypeLocator = "MFFIncludedTypeLocator";

        public const string MFFAttributeTypeLocator = "MFFAttributeTypeLocator";
    }
}