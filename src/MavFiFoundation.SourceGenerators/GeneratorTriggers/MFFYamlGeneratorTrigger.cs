// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using MavFiFoundation.SourceGenerators.Serializers;

namespace MavFiFoundation.SourceGenerators.GeneratorTriggers;

/// <summary>
/// Used to trigger code generation based yaml files where the file name ends with .CodeGen.yaml 
/// or .CodeGen.yml.
/// </summary>
/// <example>
/// <code>
/// srcLocatorType: MFFAttributeTypeLocator
/// srcLocatorInfo: Examples.GenerateExampleAttribute
/// srcOutputInfos:
///   - fileNameBuilderInfo: '{{ srcType.Name }}_Generated.g.cs'
///     sourceBuilderType: MFFLiquidBuilder
///     sourceBuilderInfo: |-
///       #nullable enable
/// 
///       public partial class {{ srcType.Name }}_Generated
///       {
/// 
///       }
/// </code>
/// </example>
public class MFFYamlGeneratorTrigger : MFFFileGeneratorTriggerBase
{
    /// <inheritdoc cref="MFFAttributeGeneratorTrigger.DefaultName"/>
    public const string DefaultName = nameof(MFFYamlGeneratorTrigger);

    /// <summary>
    /// Default regex string for the fileNameSuffix passed to 
    /// <see cref="MFFFileGeneratorTriggerBase.MFFFileGeneratorTriggerBase(string, string, IMFFSerializer)"/> .
    /// </summary>
    public const string DefaultFileNameSuffix = ".CodeGen.y(a?)ml";

    /// <inheritdoc cref="MFFFileGeneratorTriggerBase.MFFFileGeneratorTriggerBase(string, string, IMFFSerializer)" 
    /// path="/param[@name='serializer']"/>
    public MFFYamlGeneratorTrigger(IMFFSerializer serializer) : base(
        DefaultName,
        DefaultFileNameSuffix,
        serializer)
    { }
    
}
