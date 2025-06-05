// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using System.Collections.Immutable;
using System.Linq.Expressions;

using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.ResourceLoaders;
using MavFiFoundation.SourceGenerators.TypeLocators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MavFiFoundation.SourceGenerators.GeneratorTriggers;

/// <summary>
/// The base class that all <see cref="IMFFGeneratorTrigger"/> implementations SHOULD implement.
/// </summary>
public abstract class MFFGeneratorTriggerBase : MFFGeneratorPluginBase
{
    /// <summary>
    /// Constructor for <see cref="MFFGeneratorTriggerBase"/>
    /// </summary>
    /// <inheritdoc cref="MFFGeneratorPluginBase.MFFGeneratorPluginBase(string)" path="/param[@name='name']"/>
    public MFFGeneratorTriggerBase(string name) : base(name) { }

    /// <summary>
    /// Used to load properties on an <see cref="MFFGeneratorInfoModel"/> from external sources.
    /// </summary>
    /// <inheritdoc cref="IMFFGeneratorTrigger.GetGeneratorInfosProvider(Microsoft.CodeAnalysis.IncrementalGeneratorInitializationContext, Microsoft.CodeAnalysis.IncrementalValuesProvider{MFFTypeSymbolSources}, Microsoft.CodeAnalysis.IncrementalValuesProvider{MFFResourceRecord}, IEnumerable{IMFFResourceLoader})" path="/param[@name='allResources']"/>
    /// <inheritdoc cref="IMFFGeneratorTrigger.GetGeneratorInfosProvider(Microsoft.CodeAnalysis.IncrementalGeneratorInitializationContext, Microsoft.CodeAnalysis.IncrementalValuesProvider{MFFTypeSymbolSources}, Microsoft.CodeAnalysis.IncrementalValuesProvider{MFFResourceRecord}, IEnumerable{IMFFResourceLoader})" path="/param[@name='resourceLoaders']"/>
    /// <param name="genInfo">Generator configuration information.</param>
    /// <inheritdoc cref="IMFFGeneratorHelper.ProcessNamespace" path="/param[@name='cancellationToken']"/>
    protected virtual void LoadResources(
        MFFGeneratorInfoModel genInfo,
        ImmutableArray<MFFResourceRecord> allResources,
        IEnumerable<IMFFResourceLoader> resourceLoaders,
        CancellationToken cancellationToken)
    {
        object? srcLocatorInfo = genInfo.SrcLocatorInfo;

        foreach (var resourceLoader in resourceLoaders)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (resourceLoader.TryLoadResource(ref srcLocatorInfo, allResources, cancellationToken))
            {
                genInfo.SrcLocatorInfo = srcLocatorInfo;
            }

            if (genInfo.SrcOutputInfos is not null)
            {
                foreach (var outputInfo in genInfo.SrcOutputInfos)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    object? srcOutputInfo = outputInfo.SourceBuilderInfo;

                    if (resourceLoader.TryLoadResource(
                        ref srcOutputInfo,
                        allResources,
                        cancellationToken))
                    {
                        outputInfo.SourceBuilderInfo = srcOutputInfo;
                    }
                }
            }
        }
    }

    /// <inheritdoc/>
    public virtual MFFGeneratorInfoModel? ValidateAdditionalFile(AdditionalFileAnalysisContext context)
    {
        // No additional file validation by default.
        // This method should be overridden by derived classes to add additional file validation.
        return null;
    }

    /// <inheritdoc/>
    public virtual MFFGeneratorInfoModel? GetGenInfo(SymbolAnalysisContext context)
    {
        // No symbol validation by default.
        // This method should be overridden by derived classes to add symbol validation.
        return null;
    }


    /// Generates and adds code actions for existing type locators, either as constants or string literals,
    /// based on the provided <paramref name="PluginsProvider"/>.
    /// </summary>
    /// <param name="codeActions">
    /// A reference to the collection of <see cref="MFFCodeAction"/> objects to which new code actions will be added.
    /// </param>
    /// <param name="syntaxNode">
    /// The <see cref="SyntaxNode"/> context for which the code actions are being generated.
    /// </param>
    /// <param name="typeLocators">
    /// An collection of <see cref="IMFFTypeLocator"/> with available type locators.
    /// </param>
    /// <param name="includeConstants">
    /// If <c>true</c>, code actions for using type locator constants will be included; otherwise, only string literal code actions are added.
    /// </param>
    protected virtual void GetExistingTypeLocatorsCodeActions(
        ref IEnumerable<MFFCodeAction>? codeActions,
        SyntaxNode syntaxNode,
        ICollection<IMFFTypeLocator> typeLocators,
        bool includeConstants = false)
    {
        ICollection<MFFCodeAction> codeActions2Add = new List<MFFCodeAction>();

        if (includeConstants)
        {
            foreach (var typeLocator in typeLocators)
            {
                codeActions2Add.Add(new MFFCodeAction(
                $"Use \"{typeLocator.Name}\" constant",
                SyntaxFactory.AttributeArgument(
                    SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                            SyntaxFactory.IdentifierName($"GeneratorConstants"),
                            SyntaxFactory.IdentifierName($"TypeLocator")),
                        SyntaxFactory.IdentifierName($"MFFAttributeTypeLocator"))),
                    $"Use \"{typeLocator.Name}\" constant"
                ));
            }
        }

        foreach (var typeLocator in typeLocators)
        {
            codeActions2Add.Add(new MFFCodeAction(
            $"Use \"{typeLocator.Name}\" literal",
            SyntaxFactory.AttributeArgument(
                SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression,
                    SyntaxFactory.Literal(typeLocator.Name))),
                $"Use \"{typeLocator.Name}\" literal"
            ));
        }

        AddCodeActions(ref codeActions, codeActions2Add);
    }
}
