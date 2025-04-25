using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MavFiFoundation.SourceGenerators;

public abstract class MFFGeneratorAnalyzerBase : DiagnosticAnalyzer
{
    public const string DiagnosticId = "MY0002";

    private const string Title = "Blocks should use braces";
    private const string MessageFormat = "Blocks should use braces";
    private const string Description = "When possible, use curly braces on code blocks.";
    private const string Category = "CodeStyle";

    private static readonly DiagnosticDescriptor Rule = new(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

    public MFFGeneratorAnalyzerBase()
    {
    }

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics 
        => ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);

        /*
        context.RegisterSymbolAction(context =>
        {
            if (context.Symbol is INamedTypeSymbol namedTypeSymbol)
			{
				var ttt = namedTypeSymbol.GetTypeSymbolRecord();
			}  
        }, SymbolKind.NamedType);
        */
        
        context.RegisterSyntaxNodeAction(context =>
        {
            var embeddedStatement = GetEmbeddedStatement(context.Node);
            if (embeddedStatement is not BlockSyntax)
            {
                context.ReportDiagnostic(Diagnostic.Create(
                    Rule,
                    context.Node.GetLocation()));

                return;
            }
        }, SyntaxKind.IfStatement, SyntaxKind.ForEachStatement, SyntaxKind.ForStatement);
    }

    public static StatementSyntax? GetEmbeddedStatement(SyntaxNode node)
        => node switch
        {
            ForEachStatementSyntax forEachStatement => forEachStatement.Statement,
            IfStatementSyntax ifStatement => ifStatement.Statement,
            ForStatementSyntax forStatement => forStatement.Statement,
            _ => null
        };
}

