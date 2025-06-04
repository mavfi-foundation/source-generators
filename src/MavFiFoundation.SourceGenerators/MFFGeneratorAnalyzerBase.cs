using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

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
                    var diagnostics = generatorTrigger.Validate(mffContext, context.AdditionalFile, genInfo, generatorTrigger);
                    diagnostics!.ReportDiagnostics(context);

                    foreach (var plugin in
                        ((IEnumerable<IMFFGeneratorPlugin>)PluginsProvider.TypeLocators.Values)
                        .Concat(PluginsProvider.Builders.Values))
                    {
                        context.CancellationToken.ThrowIfCancellationRequested();
                        diagnostics = plugin.Validate(mffContext, context.AdditionalFile, genInfo, generatorTrigger);
                        diagnostics!.ReportDiagnostics(context);
                    }
                }
            }
        });

        context.RegisterSymbolAction(context =>
        {
            foreach (var generatorTrigger in PluginsProvider.GeneratorTriggers.Values)
            {
                var genInfo = generatorTrigger.GetGenInfo(context);

                if (genInfo is not null)
                {
                    var mffContext = new MFFAnalysisContext(context.Compilation, context.CancellationToken);
                    var diagnostics = generatorTrigger.Validate(mffContext, context.Symbol, genInfo, generatorTrigger);
                    diagnostics!.ReportDiagnostics(context);

                    foreach (var plugin in
                        ((IEnumerable<IMFFGeneratorPlugin>)PluginsProvider.TypeLocators.Values)
                        .Concat(PluginsProvider.Builders.Values))
                    {
                        context.CancellationToken.ThrowIfCancellationRequested();
                        diagnostics = plugin.Validate(mffContext, context.Symbol, genInfo, generatorTrigger);
                        diagnostics!.ReportDiagnostics(context);
                    }
                }
            }
        }, SymbolKind.NamedType);
    }
    #endregion
}

