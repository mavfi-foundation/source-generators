using MavFiFoundation.SourceGenerators.Testing;
using MavFiFoundation.SourceGenerators.TestSupport;

namespace MavFiFoundation.SourceGenerators.IntegrationTests;
public class MFFGeneratorTestDataGenerator : MFFGeneratorTestDataProviderBase
{

    public MFFGeneratorTestDataGenerator()
    {
        var testDataBuilder = new GeneratorTestDataBuilder<MFFGeneratorXUnitTestData>();
 
        var generatorType = typeof(MFFGenerator);

        // NothingIn_NothingOut
        testDataBuilder.BeginTest(
            "NothingIn_NothingOut");

        testDataBuilder.AddSource(string.Empty);

        _data.Add(testDataBuilder.BuildTestData());

        // MFFAttributeGeneratorTrigger_IncludedTypeLocator_ScribanBuilder_GeneratesClass
        testDataBuilder.BeginTest(
            "MFFAttributeGeneratorTrigger_IncludedTypeLocator_ScribanBuilder_GeneratesClass");

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.MFFAttributeGeneratorTriggerIncludedTypeLocatorScribanBuilderGeneratesClass, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddGeneratedSource((
            generatorType,
            Constants.SourceFiles.OutputFileName.MFFAttributeGeneratorTriggerIncludedTypeLocatorScribanBuilderGeneratesClass,
             EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.GeneratedCode.MFFAttributeGeneratorTriggerIncludedTypeLocatorScribanBuilderGeneratesClass,
                EmbeddedResourceHelper.EmbeddedResourceType.GeneratedCode)
        ));

        testDataBuilder.AddGeneratedSource((
            generatorType,
            Constants.SourceFiles.OutputFileName.MFFAttributeGeneratorTriggerIncludedTypeLocatorScribanBuilderGeneratesClass2,
             EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.GeneratedCode.MFFAttributeGeneratorTriggerIncludedTypeLocatorScribanBuilderGeneratesClass2,
                EmbeddedResourceHelper.EmbeddedResourceType.GeneratedCode)
        ));

        _data.Add(testDataBuilder.BuildTestData());

        // MFFAttributeGeneratorTrigger_AttributeTypeLocator_ScribanBuilder_GeneratesClass
        testDataBuilder.BeginTest(
            "MFFAttributeGeneratorTrigger_AttributeTypeLocator_ScribanBuilder_GeneratesClass");

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.MFFAttributeGeneratorTriggerAttributeTypeLocatorScribanBuilderGeneratesClassAttribute, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.MFFAttributeGeneratorTriggerAttributeTypeLocatorScribanBuilderGeneratesClassGenerate, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.MFFAttributeGeneratorTriggerAttributeTypeLocatorScribanBuilderGeneratesClassSource, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddGeneratedSource((
            generatorType,
            Constants.SourceFiles.OutputFileName.MFFAttributeGeneratorTriggerAttributeTypeLocatorScribanBuilderGeneratesClass,
             EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.GeneratedCode.MFFAttributeGeneratorTriggerAttributeTypeLocatorScribanBuilderGeneratesClass,
                EmbeddedResourceHelper.EmbeddedResourceType.GeneratedCode)
        ));

        _data.Add(testDataBuilder.BuildTestData());

        // MFFJsonGeneratorTrigger_DynamicLinqTypeLocator_LiquidBuilder_GeneratesClass
        testDataBuilder.BeginTest(
            "MFFJsonGeneratorTrigger_DynamicLinqTypeLocator_LiquidBuilder_GeneratesClass");

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.MFFJsonGeneratorTriggerDynamicLinqTypeLocatorLiquidBuilderGeneratesClassAttribute, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.MFFJsonGeneratorTriggerDynamicLinqTypeLocatorLiquidBuilderGeneratesClassSource, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddAdditionalFile((
            Constants.SourceFiles.AdditionalFile.MFFJsonGeneratorTriggerDynamicLinqTypeLocatorLiquidBuilderGeneratesClassGenerate,
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.AdditionalFile.MFFJsonGeneratorTriggerDynamicLinqTypeLocatorLiquidBuilderGeneratesClassGenerate, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code)));

        testDataBuilder.AddGeneratedSource((
            generatorType,
            Constants.SourceFiles.OutputFileName.MFFJsonGeneratorTriggerDynamicLinqTypeLocatorLiquidBuilderGeneratesClass,
             EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.GeneratedCode.MFFJsonGeneratorTriggerDynamicLinqTypeLocatorLiquidBuilderGeneratesClass,
                EmbeddedResourceHelper.EmbeddedResourceType.GeneratedCode)
        ));

        _data.Add(testDataBuilder.BuildTestData());

        // MFFXmlGeneratorTrigger_AttributeTypeLocator_ScribanBuilder_GeneratesClass
        testDataBuilder.BeginTest(
            "MFFXmlGeneratorTrigger_AttributeTypeLocator_ScribanBuilder_GeneratesClass");

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.MFFXmlGeneratorTriggerAttributeTypeLocatorScribanBuilderGeneratesClassAttribute, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.MFFXmlGeneratorTriggerAttributeTypeLocatorScribanBuilderGeneratesClassSource, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddAdditionalFile((
            Constants.SourceFiles.AdditionalFile.MFFXmlGeneratorTriggerAttributeTypeLocatorScribanBuilderGeneratesClassGenerate,
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.AdditionalFile.MFFXmlGeneratorTriggerAttributeTypeLocatorScribanBuilderGeneratesClassGenerate, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code)));

        testDataBuilder.AddGeneratedSource((
            generatorType,
            Constants.SourceFiles.OutputFileName.MFFXmlGeneratorTriggerAttributeTypeLocatorScribanBuilderGeneratesClass,
             EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.GeneratedCode.MFFXmlGeneratorTriggerAttributeTypeLocatorScribanBuilderGeneratesClass,
                EmbeddedResourceHelper.EmbeddedResourceType.GeneratedCode)
        ));

        _data.Add(testDataBuilder.BuildTestData());

        // MFFYamlGeneratorTrigger_AttributeTypeLocator_LiquidBuilder_GeneratesClass
        testDataBuilder.BeginTest(
            "MFFYamlGeneratorTrigger_AttributeTypeLocator_LiquidBuilder_GeneratesClass");

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.MFFYamlGeneratorTriggerAttributeTypeLocatorLiquidBuilderGeneratesClassAttribute, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.MFFYamlGeneratorTriggerAttributeTypeLocatorLiquidBuilderGeneratesClassSource, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddAdditionalFile((
            Constants.SourceFiles.AdditionalFile.MFFYamlGeneratorTriggerAttributeTypeLocatorLiquidBuilderGeneratesClassGenerate,
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.AdditionalFile.MFFYamlGeneratorTriggerAttributeTypeLocatorLiquidBuilderGeneratesClassGenerate, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code)));

        testDataBuilder.AddGeneratedSource((
            generatorType,
            Constants.SourceFiles.OutputFileName.MFFYamlGeneratorTriggerAttributeTypeLocatorLiquidBuilderGeneratesClass,
             EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.GeneratedCode.MFFYamlGeneratorTriggerAttributeTypeLocatorLiquidBuilderGeneratesClass,
                EmbeddedResourceHelper.EmbeddedResourceType.GeneratedCode)
        ));

        _data.Add(testDataBuilder.BuildTestData());

        // ResourceLoader_LoadsIncludedResources
        testDataBuilder.BeginTest(
            "ResourceLoader_LoadsIncludedResources");

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.ResourceLoaderLoadsIncludedResources, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddAdditionalFile((
            Constants.SourceFiles.AdditionalFile.TestTemplate,
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.AdditionalFile.TestTemplate, 
                EmbeddedResourceHelper.EmbeddedResourceType.AdditionalFiles)));

        testDataBuilder.AddGeneratedSource((
            generatorType,
            Constants.SourceFiles.OutputFileName.ResourceLoaderLoadsIncludedResources,
             EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.GeneratedCode.ResourceLoaderLoadsIncludedResources,
                EmbeddedResourceHelper.EmbeddedResourceType.GeneratedCode)
        ));

        _data.Add(testDataBuilder.BuildTestData());

        // ResourceLoader_LoadsEmbeddedResources
        testDataBuilder.BeginTest(
            "ResourceLoader_LoadsEmbeddedResources");

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.ResourceLoaderLoadsEmbeddedResources, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddGeneratedSource((
            generatorType,
            Constants.SourceFiles.OutputFileName.ResourceLoaderLoadsEmbeddedResources,
             EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.GeneratedCode.ResourceLoaderLoadsEmbeddedResources,
                EmbeddedResourceHelper.EmbeddedResourceType.GeneratedCode)
        ));

        _data.Add(testDataBuilder.BuildTestData());

        // CreateGeneratorConstants
        testDataBuilder.BeginTest(
            "CreateGeneratorConstants");

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.CreateGeneratorConstants, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddGeneratedSource((
            generatorType,
            Constants.SourceFiles.OutputFileName.CreateGeneratorConstants,
             EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.GeneratedCode.CreateGeneratorConstants,
                EmbeddedResourceHelper.EmbeddedResourceType.GeneratedCode)
        ));

        _data.Add(testDataBuilder.BuildTestData());

        // All
        _data.Add(testDataBuilder.BuildAllTestData());       
    }
}

