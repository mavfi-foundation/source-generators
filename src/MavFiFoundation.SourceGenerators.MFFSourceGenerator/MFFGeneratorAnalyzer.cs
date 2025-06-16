using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;

namespace MavFiFoundation.SourceGenerators;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class MFFGeneratorAnalyzer : MFFGeneratorAnalyzerBase
{
    public MFFGeneratorAnalyzer () : base(
        new MFFGeneratorPluginsProvider())
    {
        
    }

}
