// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using System.Collections.Immutable;
using System.Reflection;
using System.Text;
using MavFiFoundation.SourceGenerators.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MavFiFoundation.SourceGenerators;

public abstract class MFFGeneratorHelperBase : IMFFGeneratorHelper
{

    #region Private/Protected Methods

    protected static string ReadEmbeddedTemplate(string name)
	{		
		var execAssembly = Assembly.GetExecutingAssembly();
		var resName = $"{ execAssembly.GetName().Name }.Templates.{ name }";
		using var stream = execAssembly.GetManifestResourceStream(resName)!;  	
		using var sr = new StreamReader(stream, Encoding.UTF8);  
		return sr.ReadToEnd();
	}

    #endregion

    #region IMFFGeneratorHelper Implementation

    /// <inheritdoc />
    public IncrementalValuesProvider<MFFTypeSymbolSources>
        GetAllTypesProvider(IncrementalGeneratorInitializationContext initContext)
    {
		var selfTypeProvider = initContext.SyntaxProvider
			.CreateSyntaxProvider(
				static (node, _) => node is TypeDeclarationSyntax,
				static (context, cancellationToken) =>
				{
					var symbol = context.SemanticModel.GetDeclaredSymbol((TypeDeclarationSyntax)context.Node, cancellationToken);
                    
                    if (symbol is INamedTypeSymbol namedTypeSymbol)
					{
						return namedTypeSymbol.GetTypeSymbolRecord();
					}

                    return null;
				})
			.Where(static _ => _ is not null);

        var assembliesTypeProvider = initContext
            .CompilationProvider.SelectMany((comp, cancellationToken) =>
            {
                var srcsAndTypesBuilder = ImmutableArray
                    .CreateBuilder<MFFTypeSymbolSources>();

//TODO: Add work item to potentially add MSBuild prop to limit assemblies added
                foreach(var assemblySymbol in comp.SourceModule.ReferencedAssemblySymbols)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if(assemblySymbol is not null)
                    {
                        var typeBuilder = ImmutableArray
                            .CreateBuilder<MFFTypeSymbolRecord>();

                        ProcessNamespace(
                            assemblySymbol.GlobalNamespace, 
                            (t) => typeBuilder.Add(t.GetTypeSymbolRecord()),
                            cancellationToken);

                        srcsAndTypesBuilder.Add(new MFFTypeSymbolSources
                        (
                            assemblySymbol.Name, 
                            typeBuilder.ToImmutable()
                        ));
                    }
                }

                return srcsAndTypesBuilder.ToImmutable();
            });

        var combined = assembliesTypeProvider.Collect().Combine(selfTypeProvider.Collect())
            .SelectMany((types, cancellationToken)=>
            {
                var srcsAndTypesBuilder = ImmutableArray
                    .CreateBuilder<MFFTypeSymbolSources>();


                var selfTypeBuilder = ImmutableArray
                    .CreateBuilder<MFFTypeSymbolRecord>();

                foreach (var type in types.Right)
                {
                    if (type is not null)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        selfTypeBuilder.Add(type);
                    }
                }

                // Add types from project being built
                srcsAndTypesBuilder
                    .AddRange(new MFFTypeSymbolSources
                    (
                        MFFGeneratorConstants.Generator.CompilingProject, 
                        selfTypeBuilder.ToImmutable()
                    ));

                // Add types from referenced assemblies
                srcsAndTypesBuilder.AddRange(types.Left);

                return srcsAndTypesBuilder.ToImmutable();
            });

            return combined;
    }

    public void ProcessNamespace(INamespaceSymbol ns2Process,
        Action<INamedTypeSymbol> typeProcessor,
        CancellationToken cancellationToken)
    {
        foreach(var symbol in ns2Process.GetMembers())
        {
            cancellationToken.ThrowIfCancellationRequested();

            if(symbol is INamespaceSymbol @namespace)
            {
                ProcessNamespace(@namespace, typeProcessor, cancellationToken);
            }
            else if(symbol is INamedTypeSymbol @type)
            {
                typeProcessor.Invoke(@type);
            }
        }
    }

    public IncrementalValuesProvider<MFFResourceRecord>
        GetAllResourcesProvider(IncrementalGeneratorInitializationContext initContext)
    {
        var textsProvider = initContext.AdditionalTextsProvider.Select((text, cancellationToken) => 
        {
            return new MFFResourceRecord(
                text.Path.Replace('\\','/'),
                text.GetText(cancellationToken)?.ToString() ?? string.Empty
            );
        });

        var embeddedProvider = initContext.SyntaxProvider
            .ForAttributeWithMetadataName(
                MFFGeneratorConstants.Generator.EmbeddedResourceAttributeName,
                static (syntaxNode, cancellationToken) => syntaxNode is TypeDeclarationSyntax,
                static (context, cancellationToken) =>
            {
                var type = context.TargetSymbol.ContainingType;
                var resource = type.GetMembers()
                                    .OfType<IFieldSymbol>()
                                    .FirstOrDefault(m => m.Name.ToLower() == "resource");

                var text = resource.ConstantValue as string;

                return new MFFResourceRecord(
                    context.TargetSymbol.ContainingType.GetFullyQualifiedName(),
                    text ?? string.Empty
                );   
            });

        var assemblyProvider = initContext
            .CompilationProvider.SelectMany((comp, cancellationToken) =>
            {
                var resourceBuilder = ImmutableArray
                    .CreateBuilder<MFFResourceRecord>();

                foreach(var assemblySymbol in comp.SourceModule.ReferencedAssemblySymbols)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if(assemblySymbol is not null)
                    {
                        ProcessNamespace(assemblySymbol.GlobalNamespace, 
                            (type) => 
                            { 
                                if (type.GetAttributes()
                                    .FirstOrDefault(t=>t.AttributeClass?
                                        .ToDisplayString() == MFFGeneratorConstants.Generator
                                            .EmbeddedResourceAttributeName) is not null)
                                {
                                    var resource = type.GetMembers()
                                        .OfType<IFieldSymbol>()
                                        .FirstOrDefault(m => m.Name.ToLower() == "resource");

                                    var text = resource.ConstantValue as string;

                                    if (text is not null)
                                    {
                                        resourceBuilder.Add(new MFFResourceRecord(
                                            type.GetFullyQualifiedName(), text));
                                    }
                                }
                            },
                            cancellationToken);
                    }
                }

                return resourceBuilder.ToImmutable();
            });

            var combined = textsProvider.Collect()
                .Combine(assemblyProvider.Collect())
                .Combine(embeddedProvider.Collect())
                .SelectMany((resources, _) => 
                {
                    return resources.Left.Left
                        .AddRange(resources.Left.Right)
                        .AddRange(resources.Right);
                });

            return combined;

    }

    public abstract IncrementalValuesProvider<MFFGeneratorInfoRecord?> GetGenerateConstantsProvider(
        IncrementalGeneratorInitializationContext initContext,
        IMFFGeneratorPluginsProvider pluginsProvider);

    #endregion
}
