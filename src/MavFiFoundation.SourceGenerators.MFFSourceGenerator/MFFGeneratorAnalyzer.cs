using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MavFiFoundation.SourceGenerators;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class MFFGeneratorAnalyzer : MFFGeneratorAnalyzerBase
{

}
