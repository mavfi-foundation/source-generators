using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MavFiFoundation.SourceGenerators;

/// <summary>
/// Extendable abstract <see cref="DiagnosticAnalyzer"/> class.
/// </summary>
public abstract class MFFGeneratorAnalyzerBase : DiagnosticAnalyzer
{

    #region Private/Protected Properties

    private readonly ImmutableArray<DiagnosticDescriptor> _supportedDiagnotics;

    /// <inheritdoc cref="MFFGeneratorBase.PluginsProvider"/>
    protected IMFFGeneratorPluginsProvider PluginsProvider { get; private set; }

    #endregion

    #region Constructors

    /// <inheritdoc cref="MFFGeneratorBase(IMFFGeneratorPluginsProvider, IMFFGeneratorHelper)" path="/param[@name='pluginsProvider']"/>
    public MFFGeneratorAnalyzerBase(IMFFGeneratorPluginsProvider pluginsProvider)
    {
        PluginsProvider = pluginsProvider;

        var supportedDiagnoticsBuilder = ImmutableArray.CreateBuilder<DiagnosticDescriptor>();

        foreach (var plugin in
            ((IEnumerable<IMFFGeneratorPlugin>)PluginsProvider.GeneratorTriggers.Values)
            .Concat(PluginsProvider.TypeLocators.Values)
            .Concat(PluginsProvider.Builders.Values))
        {
            plugin.AddSupportedAnalyzerDiagnostics(supportedDiagnoticsBuilder);
        }

        _supportedDiagnotics = supportedDiagnoticsBuilder.ToImmutableArray();
    }

    #endregion

    #region DiagnosticAnalyzer Implementation

    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => _supportedDiagnotics;

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);

        context.RegisterAdditionalFileAction(context =>
        {
            foreach (var generatorTrigger in PluginsProvider.GeneratorTriggers.Values)
            {
                context.CancellationToken.ThrowIfCancellationRequested();
                var genInfo = generatorTrigger.ValidateAdditionalFile(context);

                if (genInfo is not null)
                {
                    var mffContext = new MFFAnalysisContext(context.Compilation, context.CancellationToken);
                    var diagnostics = generatorTrigger.Validate(mffContext, genInfo, generatorTrigger);
                    diagnostics!.ReportDiagnostics(context);

                    foreach (var plugin in
                        ((IEnumerable<IMFFGeneratorPlugin>)PluginsProvider.TypeLocators.Values)
                        .Concat(PluginsProvider.Builders.Values))
                    {
                        context.CancellationToken.ThrowIfCancellationRequested();
                        diagnostics = plugin.Validate(mffContext, genInfo, generatorTrigger);
                        diagnostics!.ReportDiagnostics(context);
                    }
                }
            }
        });

        context.RegisterSymbolAction(context =>
        {
            foreach (var generatorTrigger in PluginsProvider.GeneratorTriggers.Values)
            {
                var genInfo = generatorTrigger.ValidateSymbol(context);

                if (genInfo is not null)
                {
                    var mffContext = new MFFAnalysisContext(context.Compilation, context.CancellationToken);
                    var diagnostics = generatorTrigger.Validate(mffContext, genInfo, generatorTrigger);
                    diagnostics!.ReportDiagnostics(context);

                    foreach (var plugin in
                        ((IEnumerable<IMFFGeneratorPlugin>)PluginsProvider.TypeLocators.Values)
                        .Concat(PluginsProvider.Builders.Values))
                    {
                        context.CancellationToken.ThrowIfCancellationRequested();
                        diagnostics = plugin.Validate(mffContext, genInfo, generatorTrigger);
                        diagnostics!.ReportDiagnostics(context);
                    }
                }
            }
        }, SymbolKind.NamedType);

        // TODO: Remove once analyzer is fully implemented
        context.RegisterSyntaxNodeAction(context =>
        {
            var embeddedStatement = GetEmbeddedStatement(context.Node);
            if (embeddedStatement is not BlockSyntax)
            {
                context.ReportDiagnostic(Diagnostic.Create(
                    SupportedDiagnostics[0],
                    context.Node.GetLocation()));

                return;
            }
        }, SyntaxKind.IfStatement, SyntaxKind.ForEachStatement, SyntaxKind.ForStatement);
    }

    // TODO: Remove once analyzer is fully implemented
    public static StatementSyntax? GetEmbeddedStatement(SyntaxNode node)
        => node switch
        {
            ForEachStatementSyntax forEachStatement => forEachStatement.Statement,
            IfStatementSyntax ifStatement => ifStatement.Statement,
            ForStatementSyntax forStatement => forStatement.Statement,
            _ => null
        };
    #endregion
}

