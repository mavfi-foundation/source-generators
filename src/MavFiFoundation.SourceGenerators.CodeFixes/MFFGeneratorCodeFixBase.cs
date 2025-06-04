using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis.Host;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;

namespace MavFiFoundation.SourceGenerators;

public abstract class MFFGeneratorCodeFixBase : CodeFixProvider
{
    #region Private/Protected Properties

    private readonly ImmutableArray<string> _fixableDiagnosticIds;
    private readonly IDictionary<string, IMFFGeneratorPlugin> _fixableDiagnosticIdsByPlugin;

    /// <inheritdoc cref="MFFGeneratorBase.PluginsProvider"/>
    protected IMFFGeneratorPluginsProvider PluginsProvider { get; private set; }

    #endregion

    #region Constructors

    /// <inheritdoc cref="MFFGeneratorBase(IMFFGeneratorPluginsProvider, IMFFGeneratorHelper)" path="/param[@name='pluginsProvider']"/>
    public MFFGeneratorCodeFixBase(IMFFGeneratorPluginsProvider pluginsProvider)
    {
        PluginsProvider = pluginsProvider;
        _fixableDiagnosticIdsByPlugin = new SortedList<string, IMFFGeneratorPlugin>();

        var fixableDiagnosticIdsBuilder = ImmutableArray.CreateBuilder<string>();

        foreach (var plugin in
            ((IEnumerable<IMFFGeneratorPlugin>)PluginsProvider.GeneratorTriggers.Values)
            .Concat(PluginsProvider.TypeLocators.Values)
            .Concat(PluginsProvider.Builders.Values))
        {
            var pluginFixableDiagnosticIdsBuilder = ImmutableArray.CreateBuilder<string>();
            plugin.AddFixableDiagnosticIds(pluginFixableDiagnosticIdsBuilder);

            foreach (var fixableDiagnostic in pluginFixableDiagnosticIdsBuilder)
            {
                _fixableDiagnosticIdsByPlugin.Add(fixableDiagnostic, plugin);
            }

            fixableDiagnosticIdsBuilder.AddRange(pluginFixableDiagnosticIdsBuilder);

        }

        _fixableDiagnosticIds = fixableDiagnosticIdsBuilder.ToImmutableArray();
    }

    #endregion

    #region CodeFixProvider Implementation

    /// <inheritdoc/>
    public override ImmutableArray<string> FixableDiagnosticIds => _fixableDiagnosticIds;

    // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/FixAllProvider.md
    // for more information on Fix All Providers
    public sealed override FixAllProvider GetFixAllProvider()
        => null!; // Set to null do disable fix all

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var cancellationToken = context.CancellationToken;
        var root = await context.Document.GetSyntaxRootAsync(cancellationToken);

        foreach (var diagnostic in context.Diagnostics)
        {
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            // Find the node that the diagnostic is reported on
            var existingNode = root?.FindNode(diagnosticSpan);

            if (existingNode is not null &&
                _fixableDiagnosticIdsByPlugin.TryGetValue(diagnostic.Id, out var plugin))
            {
                var codeActions = plugin.GetCodeActions(diagnostic.Id, existingNode);

                if (codeActions is not null)
                {
                    foreach (var codeAction in codeActions)
                    {
                        context.RegisterCodeFix(
                            CodeAction.Create(
                                codeAction.Title,
                                (cancellationToken) => CreateChangeDocument(diagnostic.Id,
                                    context.Document,
                                    existingNode,
                                    codeAction.UpdatedNode,
                                    cancellationToken),
                                equivalenceKey: codeAction.EquivalenceKey),
                            diagnostic
                        );
                    }
                }
            }
        }
    }

    private static async Task<Document> CreateChangeDocument(
        string DiagnosticId,
        Document document,
        SyntaxNode existingNode,
        SyntaxNode updatedNode,
        CancellationToken cancellationToken)
    {
        var root = await document.GetSyntaxRootAsync(cancellationToken);

        var editor = new SyntaxEditor(existingNode, document.Project.Solution.Services);

        var updatedRootNode = root!.ReplaceNode(existingNode, updatedNode);
        editor.ReplaceNode(existingNode, updatedRootNode);

        var newRoot = editor.GetChangedRoot();
        return document.WithSyntaxRoot(newRoot);
    }

    #endregion
}
