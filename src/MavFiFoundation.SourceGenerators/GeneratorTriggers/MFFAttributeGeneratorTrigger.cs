// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using MavFiFoundation.SourceGenerators.Models;
using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.ResourceLoaders;
using MavFiFoundation.SourceGenerators.Serializers;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MavFiFoundation.SourceGenerators.GeneratorTriggers;

/// <summary>
/// Used to trigger code generation based on an attribute included on a type. By default, 
/// <see cref="MFFGenerateSourceAttribute"/> is used.
/// </summary>
/// <example>
/// All information specified directly in attribute using <see cref="MFFJsonSerializer"/> to
/// deserialize builder information.
/// <code>
/// [MFFGenerateSource(MFFAttributeTypeLocator.DEFAULT_NAME,
///     "Examples.GenerateExampleAttribute",
/// """
/// [
/// 	{
/// 		"FileNameBuilderInfo": "{{ srcType.Name }}_Generated.g.cs", 
/// """ +
/// $"\n		\"SourceBuilderType\": \"{ MFFScribanBuilder.DEFAULT_NAME }\",\n" +
/// """
/// 		"SourceBuilderInfo": "#nullable enable\n\n
/// """ +
/// """
/// public partial class {{ srcType.Name }}_Generated\n{\n\n}"
/// """ + 
/// """
/// 	}
/// ]
/// """
/// 	)]
/// public class Example
/// {
/// 
/// }
/// </code>
/// </example>
/// <example>
/// Builder information specified in external source and loaded by <see cref="MFFResourceLoader"/>.
/// <code>
/// [MFFGenerateSource(MFFAttributeTypeLocator.DEFAULT_NAME,
///     "Examples.GenerateExampleAttribute",
///     "MFFResourceLoader:ExampleOutputInfo.res.json")]
/// public class Example
/// {
/// 
/// }
/// </code>
/// </example>
public class MFFAttributeGeneratorTrigger : MFFGeneratorTriggerBase, IMFFGeneratorTrigger
{

    #region Constants
    #region Class Constants
    /// <summary>
    /// Default name used to identify the generator plugin.
    /// </summary>
    public const string DefaultName = nameof(MFFAttributeGeneratorTrigger);

    /// <summary>
    /// Default attribute name used to locate triggering types and store generator
    /// configuration information.
    /// </summary>
    public const string DefaultAttributeName = "MavFiFoundation.SourceGenerators.MFFGenerateSourceAttribute";


    /// <summary>
    /// Attribute constructor property name for srcLocatorType property
    /// </summary>
    protected const string CtorArgSrcLocatorType = "srcLocatorType";

    /// <summary>
    /// Attribute constructor property name for srcLocatorInfo property
    /// </summary>
    protected const string CtorArgSrcLocatorInfo = "srcLocatorInfo";

    /// <summary>
    /// Attribute constructor property name for useSymbolForLocatorInfo property.
    /// </summary>
    protected const string CtorArgUseSymbolForLocatorInfo = "useSymbolForLocatorInfo";

    /// <summary>
    /// Attribute constructor property name for outputInfo property
    /// </summary>
    protected const string CtorArgOutputInfo = "outputInfo";

    #endregion
    #region Diagnostic Constants

    /// <summary>
    /// Diagnotic Id for InvalidTypeLocator rule
    /// </summary>
    public const string InvalidTypeLocatorDiagnosticId = "MFFSG10101";

    /// <summary>
    /// Diagnotic Title for InvalidTypeLocator rule
    /// </summary>
    private const string InvalidTypeLocatorTitle = "The specified SrcTypeLocator is blank or does not exist";

    /// <summary>
    /// Diagnotic Message Format for InvalidTypeLocator rule
    /// </summary>
    public const string InvalidTypeLocatorMessageFormat = "The specified SrcTypeLocator is blank or does not exist";

    /// <summary>
    /// Diagnotic Description for InvalidTypeLocator rule
    /// </summary>
    protected const string InvalidTypeLocatorDescription = "The specified SrcTypeLocator does not exist.";

    /// <summary>
    /// Diagnotic Id for NoOutputs rule
    /// </summary>
    public const string NoOutputsDiagnosticId = "MFFSG10102";

    /// <summary>
    /// Diagnotic Title for NoOutputs rule
    /// </summary>
    protected const string NoOutputsTitle = "At least one generator output or source output should be specified";

    /// <summary>
    /// Diagnotic Message Format for NoOutputs rule
    /// </summary>
    public const string NoOutputsMessageFormat = "At least one generator output or source output should be specified";

    /// <summary>
    /// Diagnotic Description for NoOutputs rule
    /// </summary>
    protected const string NoOutputsDescription = "At least one generator output or source output should be specified.";

    #endregion
    #endregion

    #region Private/Protected Properties

    /// <summary>
    /// Gets the attribute name used to locate triggering types and store generator
    /// configuration information.
    /// </summary>
    protected string ConfigAttributeName { get; private set; }

    protected IMFFGeneratorPluginsProvider PluginsProvider { get; private set; }

    /// <summary>
    /// Get the default serializer to use.
    /// </summary>
    protected IMFFSerializer Serializer { get; private set; }

    protected static readonly DiagnosticDescriptor InvalidTypeLocatorRule = new(
        InvalidTypeLocatorDiagnosticId,
        InvalidTypeLocatorTitle,
        InvalidTypeLocatorMessageFormat,
        DiagnosticCategory,
        DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: InvalidTypeLocatorDescription);

    protected static readonly DiagnosticDescriptor NoOutputsRule = new(
        NoOutputsDiagnosticId,
        NoOutputsTitle,
        NoOutputsMessageFormat,
        DiagnosticCategory,
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: NoOutputsDescription);

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for <see cref="MFFAttributeGeneratorTrigger"/> that uses default attribute name.
    /// </summary>
    /// <inheritdoc cref="MFFAttributeGeneratorTrigger(string, string, IMFFGeneratorPluginsProvider, IMFFSerializer)"/>
    public MFFAttributeGeneratorTrigger(
        IMFFGeneratorPluginsProvider pluginsProvider,
        IMFFSerializer serializer)
        : this(
            DefaultName,
            DefaultAttributeName,
            pluginsProvider,
            serializer)
    {

    }

    /// <summary>
    /// Constructor for <see cref="MFFAttributeGeneratorTrigger"/> that allows for a custom attribute
    /// to be used.
    /// </summary>
    /// <inheritdoc cref="MFFGeneratorPluginBase.MFFGeneratorPluginBase(string)" path="/param[@name='name']"/>
    /// <param name="configAttributeName">The attribute name used to locate triggering types and store generator
    /// configuration information.</param>
    /// <param name="pluginsProvider">The PluginsProvider to use.</param>
    /// <param name="serializer">The serializer to use.</param>
    public MFFAttributeGeneratorTrigger(
        string name,
        string configAttributeName,
        IMFFGeneratorPluginsProvider pluginsProvider,
        IMFFSerializer serializer) : base(name)
    {
        ConfigAttributeName = configAttributeName;
        PluginsProvider = pluginsProvider;
        Serializer = serializer;
    }

    #endregion

    #region IMFFGeneratorInfoLocator Implementation

    /// <inheritdoc/>
    public IncrementalValuesProvider<MFFGeneratorInfoRecord?> GetGeneratorInfosProvider(
        IncrementalGeneratorInitializationContext genContext,
        IncrementalValuesProvider<MFFTypeSymbolSources> allTypes,
        IncrementalValuesProvider<MFFResourceRecord> allResources,
        IEnumerable<IMFFResourceLoader> resourceLoaders)
    {
        var pipeline = allTypes.Collect().Combine(allResources.Collect())
            .SelectMany((combined, cancellationToken) =>
                GetTypesWithAttribute(
                    combined.Left,
                    combined.Right,
                    resourceLoaders,
                    cancellationToken));

        return pipeline;
    }

    /// <summary>
    /// Reads generator configuration information from all types that contain an attribute that
    /// matches <see cref="ConfigAttributeName"/> property.
    /// </summary>
    /// <inheritdoc cref="GetGeneratorInfosProvider" path="/param[@name='allTypes']"/>
    /// <inheritdoc cref="GetGeneratorInfosProvider" path="/param[@name='allResources']"/>
    /// <inheritdoc cref="GetGeneratorInfosProvider" path="/param[@name='resourceLoaders']"/>
    /// <inheritdoc cref="IMFFGeneratorHelper.ProcessNamespace" path="/param[@name='cancellationToken']"/>
    /// <returns>The <see cref="ImmutableArray{T}"/> containing generator configuration information.</returns>
    protected ImmutableArray<MFFGeneratorInfoRecord?> GetTypesWithAttribute(
        ImmutableArray<MFFTypeSymbolSources> allTypes,
        ImmutableArray<MFFResourceRecord> allResources,
        IEnumerable<IMFFResourceLoader> resourceLoaders,
        CancellationToken cancellationToken)
    {
        var generatorRecordsBuilder = ImmutableArray
                .CreateBuilder<MFFGeneratorInfoRecord?>();

        var selfSource = allTypes
            .Where(s => s.Source == MFFGeneratorConstants.Generator.CompilingProject)
            .FirstOrDefault();

        if (selfSource is not null)
        {

            foreach (var srcType in selfSource.Types.Where(
                t => t.Attributes.Any(a => a.Name == ConfigAttributeName)))
            {
                cancellationToken.ThrowIfCancellationRequested();

                MFFGeneratorInfoModel? sourceInfo = GetGeneratorInfoFromAttribute(srcType, cancellationToken);

                if (sourceInfo is not null)
                {
                    LoadResources(sourceInfo, allResources, resourceLoaders, cancellationToken);
                    generatorRecordsBuilder.Add(sourceInfo.ToRecord());
                }
            }
        }

        return generatorRecordsBuilder.ToImmutable();

    }

    /// Extracts generator information from a specific attribute applied to the given type symbol.
    /// </summary>
    /// <param name="srcType">
    /// The <see cref="MFFTypeSymbolRecord"/> representing the source type from which to extract attribute information.
    /// </param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> to observe while performing the operation.
    /// </param>
    /// <returns>
    /// An instance of <see cref="MFFGeneratorInfoModel"/> populated with information from the attribute.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the expected attribute is not found on the source type.
    /// </exception>
    protected MFFGeneratorInfoModel GetGeneratorInfoFromAttribute(
        MFFTypeSymbolRecord srcType,
        CancellationToken cancellationToken)
    {
        var sourceInfo = new MFFGeneratorInfoModel();

        sourceInfo.ContainingNamespace = srcType.ContainingNamespace;

        var att = srcType.Attributes.First(a => a.Name == ConfigAttributeName);

        foreach (var attProp in att.Properties
            .Where(p => p.From == MFFAttributePropertyLocationType.Constructor))
        {
            cancellationToken.ThrowIfCancellationRequested();

            switch (attProp.Name)
            {
                case CtorArgSrcLocatorType:
                    sourceInfo.SrcLocatorType = (string?)attProp.Value;
                    break;
                case CtorArgSrcLocatorInfo:
                    sourceInfo.SrcLocatorInfo = (string?)attProp.Value;
                    break;
                case CtorArgUseSymbolForLocatorInfo:
                    var useSymbol = attProp.Value == null ? false : (bool)attProp.Value;
                    if (useSymbol)
                    {
                        sourceInfo.SrcLocatorInfo = srcType;
                    }
                    break;
                case CtorArgOutputInfo:
                    var outputInfo = (string?)attProp.Value;
                    if (outputInfo is not null)
                    {
                        sourceInfo.SrcOutputInfos = Serializer.DeserializeObject<List<MFFBuilderModel>?>(outputInfo);
                    }
                    break;
                default: //do nothing
                    break;
            }
        }

        return sourceInfo;
    }

    /// <inheritdoc/>
    public override void AddSupportedAnalyzerDiagnostics(ImmutableArray<DiagnosticDescriptor>.Builder supportedDiagnoticsBuilder)
    {
        base.AddSupportedAnalyzerDiagnostics(supportedDiagnoticsBuilder);
        supportedDiagnoticsBuilder.Add(InvalidTypeLocatorRule);
        supportedDiagnoticsBuilder.Add(NoOutputsRule);
    }

    /// <inheritdoc/>
    public override void AddFixableDiagnosticIds(ImmutableArray<string>.Builder fixableDiagnosticIdsBuilder)
    {
        base.AddFixableDiagnosticIds(fixableDiagnosticIdsBuilder);
        fixableDiagnosticIdsBuilder.Add(InvalidTypeLocatorDiagnosticId);
    }

    /// <inheritdoc/>
    public override MFFGeneratorInfoModel? GetGenInfo(SymbolAnalysisContext context)
    {
        if (context.Symbol is INamedTypeSymbol namedTypeSymbol)
        {
            var srcType = namedTypeSymbol.GetTypeSymbolRecord();

            if (srcType.Attributes.Any(a => a.Name == ConfigAttributeName))
            {
                MFFGeneratorInfoModel? sourceInfo = GetGeneratorInfoFromAttribute(srcType, context.CancellationToken);
                return sourceInfo;
            }
        }

        return null;
    }

    /// <inheritdoc/>
    public override IEnumerable<Diagnostic>? Validate(MFFAnalysisContext context, object source, MFFGeneratorInfoModel genInfo, IMFFGeneratorTrigger generatorTrigger)
    {
        var ret = base.Validate(context, source, genInfo, generatorTrigger);

        var att = ((ISymbol)source)
            .GetAttributes()
            .Where(a => a.AttributeClass?.ToDisplayString() == ConfigAttributeName)
            .FirstOrDefault();

        var syntax = att.ApplicationSyntaxReference?.GetSyntax() as AttributeSyntax;

        if (att is not null && syntax is not null)
        {
            var attArgList = syntax.ChildNodes()
                .Where(cn => cn.IsKind(SyntaxKind.AttributeArgumentList))
                .First() as AttributeArgumentListSyntax;

            if (attArgList is not null)
            {
                // The specified SrcTypeLocator is blank or does not exist
                if (genInfo.SrcLocatorType is null ||
                    string.IsNullOrWhiteSpace(genInfo.SrcLocatorType) ||
                    !PluginsProvider.TypeLocators.ContainsKey(genInfo.SrcLocatorType))
                {
                    var srcLocatorTypeParam = att.AttributeConstructor?.Parameters
                        .Where(p => p.Name == CtorArgSrcLocatorType)
                        .FirstOrDefault();

                    if (srcLocatorTypeParam is not null)
                    {
                        var location = attArgList.Arguments[srcLocatorTypeParam.Ordinal].GetLocation();

                        AddDiagnostic(ref ret, Diagnostic.Create(
                                InvalidTypeLocatorRule,
                                location));
                    }
                }

                // At least one generator output or source output should be specified.
                if ((genInfo.GenOutputInfos is null || !genInfo.GenOutputInfos.Any()) &&
                    (genInfo.SrcOutputInfos is null || !genInfo.SrcOutputInfos.Any()))
                {

                    var location = attArgList.GetLocation();

                    AddDiagnostic(ref ret, Diagnostic.Create(
                            NoOutputsRule,
                            location));
                }
            }
        }

        return ret;
    }

    /// <inheritdoc/>
    public override IEnumerable<MFFCodeAction>? GetCodeActions(string diagnosticId, SyntaxNode syntaxNode)
    {
        var ret = base.GetCodeActions(diagnosticId, syntaxNode);

        switch (diagnosticId)
        {
            case InvalidTypeLocatorDiagnosticId:
                GetExistingTypeLocatorsCodeActions(
                    ref ret, syntaxNode, PluginsProvider.TypeLocators.Values, true);
                break;
            default:
                // No code actions for other diagnostics
                break;
        }
        
        return ret;
    }

    #endregion
}
