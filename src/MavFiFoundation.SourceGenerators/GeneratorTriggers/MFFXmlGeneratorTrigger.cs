// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using System.Collections.Immutable;

using MavFiFoundation.SourceGenerators.Serializers;

using Microsoft.CodeAnalysis;

namespace MavFiFoundation.SourceGenerators.GeneratorTriggers;

/// <summary>
/// Used to trigger code generation based xml files where the file name ends with .CodeGen.xml.
/// </summary>
/// <example>
/// <code>
/// &lt;?xml version="1.0" encoding="utf-16"?&gt;
/// &lt;MFFGeneratorInfoModel&gt;
///   &lt;SrcLocatorType&gt;MFFAttributeTypeLocator&lt;/SrcLocatorType&gt;
///   &lt;SrcLocatorInfo type="System.String"&gt;Examples.GenerateExampleAttribute&lt;/SrcLocatorInfo&gt;
///   &lt;SrcOutputInfos&gt;
///     &lt;MFFBuilderModel&gt;
///       &lt;FileNameBuilderInfo&gt;{{ srcType.Name }}_Generated.g.cs&lt;/FileNameBuilderInfo&gt;
///       &lt;SourceBuilderType&gt;MFFScribanBuilder&lt;/SourceBuilderType&gt;
///       &lt;SourceBuilderInfo type="System.String"&gt;#nullable enable
/// 
/// public partial class {{ srcType.Name }}_Generated
/// {
/// 
/// }&lt;/SourceBuilderInfo&gt;
///     &lt;/MFFBuilderModel&gt;
///   &lt;/SrcOutputInfos&gt;
/// &lt;/MFFGeneratorInfoModel&gt;
/// </code>
/// </example>
public class MFFXmlGeneratorTrigger : MFFFileGeneratorTriggerBase
{
    /// <inheritdoc cref="MFFAttributeGeneratorTrigger.DEFAULT_NAME"/>
    public const string DEFAULT_NAME = nameof(MFFXmlGeneratorTrigger);

    /// <inheritdoc cref="MFFYamlGeneratorTrigger.DEFAULT_FILE_NAME_SUFFIX"/>
    public const string DEFAULT_FILE_NAME_SUFFIX = ".CodeGen.xml";

    /// <inheritdoc cref="MFFFileGeneratorTriggerBase.MFFFileGeneratorTriggerBase(string, string, IMFFSerializer)" 
    /// path="/param[@name='serializer']"/>
    public MFFXmlGeneratorTrigger(IMFFSerializer serializer) : base(
        DEFAULT_NAME,
        DEFAULT_FILE_NAME_SUFFIX,
        serializer)
    { }

}
