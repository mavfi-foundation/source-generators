using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;

namespace MavFiFoundation.SourceGenerators;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public class MFFGeneratorCodeFix : MFFGeneratorCodeFixBase
{
    public MFFGeneratorCodeFix() : base(
        new MFFGeneratorPluginsProvider())
    {

    }
}
