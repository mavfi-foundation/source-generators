// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using MavFiFoundation.SourceGenerators.Serializers;

namespace MavFiFoundation.SourceGenerators.GeneratorTriggers;

/// <summary>
/// Used to trigger code generation based json files where the file name ends with .CodeGen.json.
/// </summary>
/// <example>
/// <code>
/// {
///     "srcLocatorType" : "MFFDynamicLinqTypeLocator",
///     "srcLocatorInfo": "Name == \"Examples.ExampleClass\"",
///     "srcOutputInfos": [{
/// 		"fileNameBuilderInfo": "{{ srcType.Name }}_Generated.g.cs", 
/// 		"sourceBuilderType": "MFFLiquidBuilder", 
/// 		"sourceBuilderInfo": "#nullable enable\n\npublic partial class {{ srcType.Name }}_Generated\n{\n\n}"
/// 	}]
/// }
/// </code>
/// </example>
public class MFFJsonGeneratorTrigger : MFFFileGeneratorTriggerBase
{
    /// <inheritdoc cref="MFFAttributeGeneratorTrigger.DefaultName"/>
    public const string DefaultName = nameof(MFFJsonGeneratorTrigger);

    /// <inheritdoc cref="MFFYamlGeneratorTrigger.DefaultFileNameSuffix"/>
    public const string DefaultFileNameSuffix = ".CodeGen.json";

    /// <inheritdoc cref="MFFFileGeneratorTriggerBase.MFFFileGeneratorTriggerBase(string, string, IMFFSerializer)" 
    /// path="/param[@name='serializer']"/>
    public MFFJsonGeneratorTrigger(IMFFSerializer serializer) : base(
        DefaultName,
        DefaultFileNameSuffix,
        serializer)
    { }

}
