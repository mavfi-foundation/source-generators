
using System.Collections;
using System.Reflection;

using Microsoft.CodeAnalysis;

namespace MavFiFoundation.SourceGenerators.Testing;

public static class Helpers
{
    private const string Additional_Source_Collection_TYPE_NAME = 
        "Microsoft.CodeAnalysis.AdditionalSourcesCollection";
    private const string SOURCE_FILE_EXTENSION = "cs";
    private const string DIAGNOSTIC_BAG_TYPE_NAME = "Microsoft.CodeAnalysis.DiagnosticBag";
    private const string SOURCES_ADDED_FIELD_NAME = "_sourcesAdded";
    private const string COUNT_PROPERTY_NAME = "Count";
    private const string HINT_NAME_PROPERTY_NAME = "HintName";
    private const string TEXT_PROPERTY_NAME = "Text";

    public static T? CreateInstance<T>(params object[] args)
    {
        var type = typeof(T);
        var instance = CreateInstance(type, args);
        return (instance is null) ? default : (T) instance;
    }

    public static object? CreateInstance(Type type, params object[] args)
    {
        var instance = type.Assembly.CreateInstance(
            type.FullName!, false,
            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
            null, args, null, null);                
        return instance;
    }

    public static Assembly GetCodeAnalysisAssembly()
    {
        var codeAnalysisAssembly = typeof(SourceProductionContext).Assembly 
            ?? throw new Exception("Unable to create CodeAnalysisAssembly");

        return codeAnalysisAssembly;
    }

    public static object CreateAdditionalSourcesCollection(Assembly? codeAnalysisAssembly = null)
    {
        if(codeAnalysisAssembly is null)
        {
            codeAnalysisAssembly = GetCodeAnalysisAssembly();
        }

        var sourcesType = codeAnalysisAssembly
            .GetType(Additional_Source_Collection_TYPE_NAME)
                ?? throw new Exception("Unable to locate AdditionalSourceCollection Type");

        var sources = Helpers.CreateInstance(sourcesType, SOURCE_FILE_EXTENSION) 
                ?? throw new Exception("Unable to create AdditionalSourceCollection");
        
        return sources;
    }

    public static object CreateDiagnosticBag(Assembly? codeAnalysisAssembly = null)
    {
        if(codeAnalysisAssembly is null)
        {
            codeAnalysisAssembly = GetCodeAnalysisAssembly();
        }

        var diagnosticsType = codeAnalysisAssembly
            .GetType(DIAGNOSTIC_BAG_TYPE_NAME)
                ?? throw new Exception("Unable to locate DiagnosticBag Type");

        var diagnostics = Helpers.CreateInstance(diagnosticsType) 
                ?? throw new Exception("Unable to create DiagnosticBag");
        
        return diagnostics;
    }

    public static SourceProductionContext CreateContext(
        Assembly? codeAnalysisAssembly = null,
        Object? sources = null,
        Object? diagnostics = null,
        CancellationToken? cancellationToken = null)
    {
        if(codeAnalysisAssembly is null)
        {
            codeAnalysisAssembly = GetCodeAnalysisAssembly();
        }

        if(sources is null)
        {
            sources = CreateAdditionalSourcesCollection(codeAnalysisAssembly);
        }

        if(diagnostics is null)
        {
            diagnostics = CreateDiagnosticBag(codeAnalysisAssembly);
        }

        if(cancellationToken is null)
        {
            cancellationToken = new CancellationToken();
        }

        return CreateInstance<SourceProductionContext>(
            sources, diagnostics, null!, cancellationToken);
    }

    public static IEnumerable GetSourcesAdded(object sources) 
    {
        var sourcesAddedFieldInfo = sources.GetType().GetField(
            SOURCES_ADDED_FIELD_NAME, BindingFlags.NonPublic | BindingFlags.Instance) ??
                throw new Exception($"Unable to get {SOURCES_ADDED_FIELD_NAME} FieldInfo");
        var sourcesAdded = sourcesAddedFieldInfo.GetValue(sources) as IEnumerable ?? 
            throw new Exception($"Unable to get {SOURCES_ADDED_FIELD_NAME} Value");

        return sourcesAdded;
    }

    public static int GetSourcesCount(object sources)
    {
        var sourcesAdded = GetSourcesAdded(sources);
        var countPropertyInfo = sourcesAdded.GetType().GetProperty(COUNT_PROPERTY_NAME) ??
            throw new Exception($"Unable to get {COUNT_PROPERTY_NAME} PropertyInfo");
        var addedSourceCount = countPropertyInfo.GetValue(sourcesAdded) as int? ?? 
            throw new Exception($"Unable to get {COUNT_PROPERTY_NAME} Value");

        return addedSourceCount;
    }

    public static bool ContainsSource(object sources, string? name = null, string? code = null)
    {
        var sourcesAdded = GetSourcesAdded(sources);
        foreach (var source in sourcesAdded)
        {
            bool nameMatch = false;
            bool codeMatch = false;

            if (name is not null)
            {
                var namePropInfo = source.GetType().GetProperty(HINT_NAME_PROPERTY_NAME) ??
                    throw new Exception($"Unable to get {HINT_NAME_PROPERTY_NAME} PropertyInfo");
                if (name == namePropInfo.GetValue(source) as string)
                {
                    nameMatch = true;
                }
            }
            else 
            {
                nameMatch = true;
            }

            if (code is not null)
            {
                var textPropInfo = source.GetType().GetProperty(TEXT_PROPERTY_NAME) ??
                    throw new Exception($"Unable to get {TEXT_PROPERTY_NAME} PropertyInfo");

                var text = textPropInfo.GetValue(source) ??
                    throw new Exception($"Unable to get {TEXT_PROPERTY_NAME} Value");
     
                if (code == text.ToString())
                {
                    codeMatch = true;
                }
            }
            else 
            {
                codeMatch = true;
            }

            if(nameMatch && codeMatch)
            {
                return true;
            }
        }

        return false;
    }
}
