// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using System.Collections;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace MavFiFoundation.SourceGenerators.Testing;

public static class TestHelpers
{
    private const string AdditionalSourceCollectionTypeName =
        "Microsoft.CodeAnalysis.AdditionalSourcesCollection";
    private const string SourceFileExtension = "cs";
    private const string DiagnosticBagTypeName = "Microsoft.CodeAnalysis.DiagnosticBag";
    private const string SourcesAddedFieldName = "_sourcesAdded";
    private const string CountPropertyName = "Count";
    private const string HintNamePropertyName = "HintName";
    private const string TextPropertyName = "Text";

    public static T? CreateInstance<T>(params object[] args)
    {
        var type = typeof(T);
        var instance = CreateInstance(type, args);
        return (instance is null) ? default : (T)instance;
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
        if (codeAnalysisAssembly is null)
        {
            codeAnalysisAssembly = GetCodeAnalysisAssembly();
        }

        var sourcesType = codeAnalysisAssembly
            .GetType(AdditionalSourceCollectionTypeName)
                ?? throw new Exception("Unable to locate AdditionalSourceCollection Type");

        var sources = TestHelpers.CreateInstance(sourcesType, SourceFileExtension)
                ?? throw new Exception("Unable to create AdditionalSourceCollection");

        return sources;
    }

    public static object CreateDiagnosticBag(Assembly? codeAnalysisAssembly = null)
    {
        if (codeAnalysisAssembly is null)
        {
            codeAnalysisAssembly = GetCodeAnalysisAssembly();
        }

        var diagnosticsType = codeAnalysisAssembly
            .GetType(DiagnosticBagTypeName)
                ?? throw new Exception("Unable to locate DiagnosticBag Type");

        var diagnostics = TestHelpers.CreateInstance(diagnosticsType)
                ?? throw new Exception("Unable to create DiagnosticBag");

        return diagnostics;
    }

    public static SourceProductionContext CreateContext(
        Assembly? codeAnalysisAssembly = null,
        Object? sources = null,
        Object? diagnostics = null,
        CancellationToken? cancellationToken = null)
    {
        if (codeAnalysisAssembly is null)
        {
            codeAnalysisAssembly = GetCodeAnalysisAssembly();
        }

        if (sources is null)
        {
            sources = CreateAdditionalSourcesCollection(codeAnalysisAssembly);
        }

        if (diagnostics is null)
        {
            diagnostics = CreateDiagnosticBag(codeAnalysisAssembly);
        }

        if (cancellationToken is null)
        {
            cancellationToken = new CancellationToken();
        }

        return CreateInstance<SourceProductionContext>(
            sources, diagnostics, null!, cancellationToken);
    }

    public static IEnumerable GetSourcesAdded(object sources)
    {
        var sourcesAddedFieldInfo = sources.GetType().GetField(
            SourcesAddedFieldName, BindingFlags.NonPublic | BindingFlags.Instance) ??
                throw new Exception($"Unable to get {SourcesAddedFieldName} FieldInfo");
        var sourcesAdded = sourcesAddedFieldInfo.GetValue(sources) as IEnumerable ??
            throw new Exception($"Unable to get {SourcesAddedFieldName} Value");

        return sourcesAdded;
    }

    public static int GetSourcesCount(object sources)
    {
        var sourcesAdded = GetSourcesAdded(sources);
        var countPropertyInfo = sourcesAdded.GetType().GetProperty(CountPropertyName) ??
            throw new Exception($"Unable to get {CountPropertyName} PropertyInfo");
        var addedSourceCount = countPropertyInfo.GetValue(sourcesAdded) as int? ??
            throw new Exception($"Unable to get {CountPropertyName} Value");

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
                var namePropInfo = source.GetType().GetProperty(HintNamePropertyName) ??
                    throw new Exception($"Unable to get {HintNamePropertyName} PropertyInfo");
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
                var textPropInfo = source.GetType().GetProperty(TextPropertyName) ??
                    throw new Exception($"Unable to get {TextPropertyName} PropertyInfo");

                var text = textPropInfo.GetValue(source) ??
                    throw new Exception($"Unable to get {TextPropertyName} Value");

                if (code == text.ToString())
                {
                    codeMatch = true;
                }
            }
            else
            {
                codeMatch = true;
            }

            if (nameMatch && codeMatch)
            {
                return true;
            }
        }

        return false;
    }
}
