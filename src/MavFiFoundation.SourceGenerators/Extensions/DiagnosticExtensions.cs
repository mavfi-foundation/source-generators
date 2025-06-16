using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace MavFiFoundation.SourceGenerators;

public static class DiagnosticExtensions
{
    /// <summary>
    /// Reports included diagnostics using the supplied <paramref name="context"/>
    /// </summary>
    /// <param name="self">The diagnostics to report.</param>
    /// <param name="context">The context to report the diagnostics on.</param>
    public static void ReportDiagnostics(
        this IEnumerable<Diagnostic>? self,
        AdditionalFileAnalysisContext context)
    {
        if (self is not null)
        {
            foreach (var diagnostic in self)
            {
                context.ReportDiagnostic(diagnostic);
            }
        }
    }

    /// <inheritdoc cref="ReportDiagnostics(IEnumerable{Diagnostic}, AdditionalFileAnalysisContext)"/>
    public static void ReportDiagnostics(
        this IEnumerable<Diagnostic>? self,
        SymbolAnalysisContext context)
    {
        if (self is not null)
        {
            foreach (var diagnostic in self)
            {
                context.ReportDiagnostic(diagnostic);
            }
        }
    }

}
