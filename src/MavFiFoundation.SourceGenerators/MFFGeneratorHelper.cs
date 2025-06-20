using MavFiFoundation.SourceGenerators.Builders;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.TypeLocators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MavFiFoundation.SourceGenerators;

public class MFFGeneratorHelper : MFFGeneratorHelperBase, IMFFGeneratorHelper
{

 
    #region IMFFGeneratorHelperBase Implementation

    public override IncrementalValuesProvider<MFFGeneratorInfoRecord?> GetGenerateConstantsProvider(
        IncrementalGeneratorInitializationContext initContext,
        IMFFGeneratorPluginsProvider pluginsProvider)
    {
        var constantsTemplate = ReadEmbeddedTemplate(
            MFFGeneratorConstants.Generator.CreateGeneratorConstantsTemplateName);

        var pipeline = initContext.SyntaxProvider.ForAttributeWithMetadataName(
            fullyQualifiedMetadataName: MFFGeneratorConstants.Generator.CreateGeneratorConstantsAttributeName,
            predicate: static (syntaxNode, cancellationToken) => syntaxNode is TypeDeclarationSyntax,
            transform: (context, cancellationToken) =>
            {
                var symbol = context.SemanticModel.GetDeclaredSymbol(
                    (TypeDeclarationSyntax)context.TargetNode, cancellationToken);
                    
                if (symbol is INamedTypeSymbol namedTypeSymbol)
				{
                    var additionalBuilderInfo = new Dictionary<string, object>
                    {
                        {
                            nameof(Builders),
                            pluginsProvider.Builders.Keys.ToArray()
                        },
                        {
                            nameof(GeneratorTriggers),
                            pluginsProvider.GeneratorTriggers.Keys.ToArray()
                        },
                        {
                            nameof(ResourceLoaders),
                            pluginsProvider.ResourceLoaders.Keys.ToArray()
                        },
                        {
                            nameof(TypeLocators),
                            pluginsProvider.TypeLocators.Keys.ToArray()
                        },
                    };

                    var sourceInfo = new MFFGeneratorInfoModel(){
                        ContainingNamespace = symbol.ContainingNamespace.ToString(),
                        SrcLocatorType = MFFIncludedTypeLocator.DefaultName,
                        SrcLocatorInfo = namedTypeSymbol.GetTypeSymbolRecord(),
                        SrcOutputInfos = [ 
                            new MFFBuilderModel() 
                            {
                                SourceBuilderType = MFFScribanBuilder.DefaultName,
                                SourceBuilderInfo = constantsTemplate,
                                AdditionalOutputInfos = additionalBuilderInfo,
                                FileNameBuilderInfo = MFFGeneratorConstants.Generator.CreateGeneratorConstantsOutputName
                            }
                        ]
                    };

                    return sourceInfo.ToRecord();
                }

                return null;
            }
        ).Where(_ => _ is not null);

        return pipeline;  
    }

    #endregion
}
