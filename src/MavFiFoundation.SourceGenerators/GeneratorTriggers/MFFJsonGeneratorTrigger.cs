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
    /// <inheritdoc cref="MFFAttributeGeneratorTrigger.DEFAULT_NAME"/>
    public const string DEFAULT_NAME = nameof(MFFJsonGeneratorTrigger);

    /// <inheritdoc cref="MFFYamlGeneratorTrigger.DEFAULT_FILE_NAME_SUFFIX"/>
    public const string DEFAULT_FILE_NAME_SUFFIX = ".CodeGen.json";

    /// <inheritdoc cref="MFFFileGeneratorTriggerBase.MFFFileGeneratorTriggerBase(string, string, IMFFSerializer)" 
    /// path="/param[@name='serializer']"/>
    public MFFJsonGeneratorTrigger(IMFFSerializer serializer) : base(
        DEFAULT_NAME,
        DEFAULT_FILE_NAME_SUFFIX,
        serializer)
    { }
}
