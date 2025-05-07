using MavFiFoundation.SourceGenerators.Testing;
using MavFiFoundation.SourceGenerators.TestSupport;

namespace MavFiFoundation.SourceGenerators.IntegrationTests;
public class MFFGeneratorTestDataGenerator : MFFGeneratorTestDataGeneratorBase
{

    public MFFGeneratorTestDataGenerator()
    {
        var testDataBuilder = new TestDataBuilder<MFFGeneratorXUnitTestData>();
 
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
                Constants.SourceFiles.Code.MFFATTRIBUTEGENERATORTRIGGER_INCLUDEDTYPELOCATOR_SCRIBANBUILDER_GENERATESCLASS, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddGeneratedSource((
            generatorType,
            Constants.SourceFiles.OutputFileName.MFFATTRIBUTEGENERATORTRIGGER_INCLUDEDTYPELOCATOR_SCRIBANBUILDER_GENERATESCLASS,
             EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.GeneratedCode.MFFATTRIBUTEGENERATORTRIGGER_INCLUDEDTYPELOCATOR_SCRIBANBUILDER_GENERATESCLASS,
                EmbeddedResourceHelper.EmbeddedResourceType.GeneratedCode)
        ));

        _data.Add(testDataBuilder.BuildTestData());

        // MFFAttributeGeneratorTrigger_AttributeTypeLocator_ScribanBuilder_GeneratesClass
        testDataBuilder.BeginTest(
            "MFFAttributeGeneratorTriggerAttributeTypeLocator_ScribanBuilder_GeneratesClass");

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.MFFATTRIBUTEGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_SCRIBANBUILDER_GENERATESCLASS_ATTRIBUTE, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.MFFATTRIBUTEGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_SCRIBANBUILDER_GENERATESCLASS_GENERATE, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.MFFATTRIBUTEGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_SCRIBANBUILDER_GENERATESCLASS_SOURCE, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddGeneratedSource((
            generatorType,
            Constants.SourceFiles.OutputFileName.MFFATTRIBUTEGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_SCRIBANBUILDER_GENERATESCLASS,
             EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.GeneratedCode.MFFATTRIBUTEGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_SCRIBANBUILDER_GENERATESCLASS,
                EmbeddedResourceHelper.EmbeddedResourceType.GeneratedCode)
        ));

        _data.Add(testDataBuilder.BuildTestData());

        // MFFJsonGeneratorTrigger_AttributeTypeLocator_LiquidBuilder_GeneratesClass
        testDataBuilder.BeginTest(
            "MFFJsonGeneratorTrigger_AttributeTypeLocator_LiquidBuilder_GeneratesClass");

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.MFFJSONGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_LIQUIDBUILDER_GENERATESCLASS_ATTRIBUTE, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.MFFJSONGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_LIQUIDBUILDER_GENERATESCLASS_SOURCE, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddAdditionalFile((
            Constants.SourceFiles.AdditionalFile.MFFJSONGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_LIQUIDBUILDER_GENERATESCLASS_GENERATE,
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.AdditionalFile.MFFJSONGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_LIQUIDBUILDER_GENERATESCLASS_GENERATE, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code)));

        testDataBuilder.AddGeneratedSource((
            generatorType,
            Constants.SourceFiles.OutputFileName.MFFJSONGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_LIQUIDBUILDER_GENERATESCLASS,
             EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.GeneratedCode.MFFJSONGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_LIQUIDBUILDER_GENERATESCLASS,
                EmbeddedResourceHelper.EmbeddedResourceType.GeneratedCode)
        ));

        _data.Add(testDataBuilder.BuildTestData());

        // MFFXmlGeneratorTrigger_AttributeTypeLocator_ScribanBuilder_GeneratesClass
        testDataBuilder.BeginTest(
            "MFFXmlGeneratorTrigger_AttributeTypeLocator_ScribanBuilder_GeneratesClass");

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.MFFXMLGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_SCRIBANBUILDER_GENERATESCLASS_ATTRIBUTE, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.MFFXMLGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_SCRIBANBUILDER_GENERATESCLASS_SOURCE, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddAdditionalFile((
            Constants.SourceFiles.AdditionalFile.MFFXMLGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_SCRIBANBUILDER_GENERATESCLASS_GENERATE,
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.AdditionalFile.MFFXMLGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_SCRIBANBUILDER_GENERATESCLASS_GENERATE, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code)));

        testDataBuilder.AddGeneratedSource((
            generatorType,
            Constants.SourceFiles.OutputFileName.MFFXMLGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_SCRIBANBUILDER_GENERATESCLASS,
             EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.GeneratedCode.MFFXMLGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_SCRIBANBUILDER_GENERATESCLASS,
                EmbeddedResourceHelper.EmbeddedResourceType.GeneratedCode)
        ));

        _data.Add(testDataBuilder.BuildTestData());

        // MFFYamlGeneratorTrigger_AttributeTypeLocator_LiquidBuilder_GeneratesClass
        testDataBuilder.BeginTest(
            "MFFYamlGeneratorTrigger_AttributeTypeLocator_LiquidBuilder_GeneratesClass");

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.MFFYAMLGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_LIQUIDBUILDER_GENERATESCLASS_ATTRIBUTE, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.MFFYAMLGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_LIQUIDBUILDER_GENERATESCLASS_SOURCE, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddAdditionalFile((
            Constants.SourceFiles.AdditionalFile.MFFYAMLGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_LIQUIDBUILDER_GENERATESCLASS_GENERATE,
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.AdditionalFile.MFFYAMLGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_LIQUIDBUILDER_GENERATESCLASS_GENERATE, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code)));

        testDataBuilder.AddGeneratedSource((
            generatorType,
            Constants.SourceFiles.OutputFileName.MFFYAMLGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_LIQUIDBUILDER_GENERATESCLASS,
             EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.GeneratedCode.MFFYAMLGENERATORTRIGGER_ATTRIBUTETYPELOCATOR_LIQUIDBUILDER_GENERATESCLASS,
                EmbeddedResourceHelper.EmbeddedResourceType.GeneratedCode)
        ));

        _data.Add(testDataBuilder.BuildTestData());

        // ResourceLoader_LoadsIncludedResources
        testDataBuilder.BeginTest(
            "ResourceLoader_LoadsIncludedResources");

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.RESOURCELOADER_LOADSINCLUDEDRESOURCES, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddAdditionalFile((
            Constants.SourceFiles.AdditionalFile.TEST_TEMPLATE,
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.AdditionalFile.TEST_TEMPLATE, 
                EmbeddedResourceHelper.EmbeddedResourceType.AdditionalFiles)));

        testDataBuilder.AddGeneratedSource((
            generatorType,
            Constants.SourceFiles.OutputFileName.RESOURCELOADER_LOADSINCLUDEDRESOURCES,
             EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.GeneratedCode.RESOURCELOADER_LOADSINCLUDEDRESOURCES,
                EmbeddedResourceHelper.EmbeddedResourceType.GeneratedCode)
        ));

        _data.Add(testDataBuilder.BuildTestData());

        // ResourceLoader_LoadsEmbeddedResources
        testDataBuilder.BeginTest(
            "ResourceLoader_LoadsEmbeddedResources");

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.RESOURCELOADER_LOADSEMBEDDEDRESOURCES, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddGeneratedSource((
            generatorType,
            Constants.SourceFiles.OutputFileName.RESOURCELOADER_LOADSEMBEDDEDRESOURCES,
             EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.GeneratedCode.RESOURCELOADER_LOADSEMBEDDEDRESOURCES,
                EmbeddedResourceHelper.EmbeddedResourceType.GeneratedCode)
        ));

        _data.Add(testDataBuilder.BuildTestData());

        // CreateGeneratorConstants
        testDataBuilder.BeginTest(
            "CreateGeneratorConstants");

        testDataBuilder.AddSource(
            EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.Code.CREATE_GENERATOR_CONSTANTS, 
                EmbeddedResourceHelper.EmbeddedResourceType.Code));

        testDataBuilder.AddGeneratedSource((
            generatorType,
            Constants.SourceFiles.OutputFileName.CREATE_GENERATOR_CONSTANTS,
             EmbeddedResourceHelper.ReadEmbeddedSource(
                Constants.SourceFiles.GeneratedCode.CREATE_GENERATOR_CONSTANTS,
                EmbeddedResourceHelper.EmbeddedResourceType.GeneratedCode)
        ));

        _data.Add(testDataBuilder.BuildTestData());

        // All
        _data.Add(testDataBuilder.BuildAllTestData());       
    }
}

