using System.Collections.Immutable;
using MavFiFoundation.SourceGenerators.Models;
using Microsoft.CodeAnalysis;

namespace MavFiFoundation.SourceGenerators.UnitTests;

public class MFFGeneratorBaseTestClass :  MFFGeneratorBase
{
    public MFFGeneratorBaseTestClass(
        IMFFGeneratorPluginsProvider pluginsProvider,
        IMFFGeneratorHelper generatorHelper) : base(pluginsProvider, generatorHelper) 
    { 

    }

    public void ExposedCreateOutput(
        ImmutableArray<MFFGeneratorInfoWithSrcTypesRecord?> genAndSrcInfos, 
        SourceProductionContext context)
    {
        CreateOutput(genAndSrcInfos, context);
    }
}
