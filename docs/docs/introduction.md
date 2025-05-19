# Introduction

- [Introduction](#introduction)
  - [Overview](#overview)
  - [Main Source Generator Components](#main-source-generator-components)
    - [Generator Triggers](#generator-triggers)
    - [Type Locators](#type-locators)
    - [Builders](#builders)

## Overview

> [!INCLUDE [Overview](../../README.md)]

## Main Source Generator Components

Source generation involves three main components: [Generator Triggers](#generator-triggers), [Type Locators](#type-locators), and [Builders](#builders). Source generation begins with and is configured using generator triggers. Once source generation has been triggered by a generator trigger, control is passed to type locators that are used to locate the types that will be passed to builders. The builders will then create the source code. 

### Generator Triggers

Generator triggers are the starting point for source generation and are where the configuration is defined for code to be generated. Regardless of the specific generator trigger that is used, the supplied configuration information is used to create a [MFFGeneratorInfoModel](../api/MavFiFoundation.SourceGenerators.Models.MFFGeneratorInfoModel.yml). How this information is specified in depending on the specific generator trigger that is being used. The following generator triggers are included:

- [MFFAttributeGeneratorTrigger](generator-triggers/attribute-generator-trigger.md) - Use an attribute on a type to trigger source generation.
- [MFFYamlGeneratorTrigger](generator-triggers/yaml-generator-trigger.md) - Use a yaml file with a CodeGen.yml or CodeGen.yaml extension to trigger source generation.
- [MFFJsonGeneratorTrigger](generator-triggers/json-generator-trigger.md)- Use a json file with a CodeGen.json extension to trigger source generation.
- [MFFXmlGeneratorTrigger](generator-triggers/xml-generator-trigger.md)- Use a xml file with a CodeGen.xml extension to trigger source generation.

### Type Locators

Type locators are used to locate the types that will be used to generate source from. The located types are passed to a builder that can use the various parts of existing types to create source code for new types. The type locator to use is specified in the [MFFGeneratorInfoModel.SrcLocatorType](../api/MavFiFoundation.SourceGenerators.Models.MFFGeneratorInfoModel.yml#MavFiFoundation_SourceGenerators_Models_MFFGeneratorInfoModel_SrcLocatorType) property. The configuration information used by the type locator is specified in the [MFFGeneratorInfoModel.SrcLocatorInfo](../api/MavFiFoundation.SourceGenerators.Models.MFFGeneratorInfoModel.yml#MavFiFoundation_SourceGenerators_Models_MFFGeneratorInfoModel_SrcLocatorInfo) property and varies depending on the specific type locator being used. The following type locators are included:

- [MFFAttributeTypeLocator](type-locators/attribute-type-locator.md) - Use an attribute on a type to locate types.
- [MFFDynamicLinqTypeLocator](type-locators/dynamic-linq-type-locator.md) - Use a [Dynamic LINQ](https://dynamic-linq.net/overview) query to locate types.
- [MFFIncludedTypeLocator](type-locators/included-type-locator.md)- Use the type that was used by the generator trigger.

### Builders

Builder are used to generate the source. Builders are also used for generating the filename for the generated source. Configuration information for builders is supplied using [MFFBuilderModel](../api/MavFiFoundation.SourceGenerators.Models.MFFBuilderModel.yml). The builder to use for source generation is specified in the [MFFBuilderModel.SourceBuilderType](../api/MavFiFoundation.SourceGenerators.Models.MFFBuilderModel.yml#MavFiFoundation_SourceGenerators_Models_MFFBuilderModel_SourceBuilderType) property. The builder to use for filename generation is specified in the [MFFBuilderModel.FileNameBuilderType](../api/MavFiFoundation.SourceGenerators.Models.MFFBuilderModel.yml#MavFiFoundation_SourceGenerators_Models_MFFBuilderModel_FileNameBuilderType) property. The following builders are included:

- [MFFScribanBuilder](builders/scriban-builder.md) - Use a [Scriban](https://github.com/scriban/scriban/blob/master/doc/language.md) template to build source.
- [MFFLiquidBuilder](builders/liquid-builder.md) - Use a [Liquid](https://shopify.github.io/liquid/) template to build source.
