# Introduction

- [Introduction](#introduction)
  - [Overview](#overview)
  - [Main source Generator Components](#main-source-generator-components)
    - [Generator Triggers](#generator-triggers)
    - [Type Locators](#type-locators)
    - [Builders](#builders)

## Overview

Source generation involves three main components: [Generator Triggers](#generator-triggers), [Type Locators](#type-locators), and [Builders](#builders). Source generation begins with and is configured using generator triggers. Once source generation has been triggered by a generator trigger, control is passed to type locators that are used to locate the types that will be passed to builders. The builders will then create the source code. 

## Main source Generator Components

### Generator Triggers

Generator triggers are the starting point for source generation and are where the configuration is defined for code to be generated. Regardless of the specific generator trigger that is used, the supplied configuration information is used to create a [MFFGeneratorInfoModel](../api/MavFiFoundation.SourceGenerators.Models.MFFGeneratorInfoModel.yml). How this information is specified in depending on the specific generator trigger that is being used. The following generator triggers are included in the MavFiFoundation.SourceGenerators project.

- [MFFAttributeGeneratorTrigger](../api/MavFiFoundation.SourceGenerators.GeneratorTriggers.MFFAttributeGeneratorTrigger.yml)
- [MFFYamlGeneratorTrigger](../api/MavFiFoundation.SourceGenerators.GeneratorTriggers.MFFYamlGeneratorTrigger.yml)
- [MFFJsonGeneratorTrigger](../api/MavFiFoundation.SourceGenerators.GeneratorTriggers.MFFJsonGeneratorTrigger.yml)
- [MFFXmlGeneratorTrigger](../api/MavFiFoundation.SourceGenerators.GeneratorTriggers.MFFXmlGeneratorTrigger.yml)

### Type Locators

### Builders
