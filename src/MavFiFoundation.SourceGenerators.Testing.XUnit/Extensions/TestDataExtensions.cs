// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;
using Xunit.Abstractions;

namespace MavFiFoundation.SourceGenerators.Testing;

public static class TestDataExtensions
{
#if NETSTANDARD2_0
   public static DiagnosticSeverity ParseDiagnosticSeverity(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return new DiagnosticSeverity();
        }
        else
        {
            switch (value)
            {
                case "Error":
                    return DiagnosticSeverity.Error;
                case "Hidden":
                    return DiagnosticSeverity.Hidden;
                case "Info":
                    return DiagnosticSeverity.Info;
                case "Warning":
                    return DiagnosticSeverity.Warning;
                default:
                    throw new NotSupportedException($"DiagnosticSeverity string '{value}'");
            }
        }
    }
#endif

    public static void DeserializeTestData(
        this MFFTestDataBase self,
        IXunitSerializationInfo info)
    {
        self.Scenario = info.GetValue<string>(nameof(MFFTestDataBase.Scenario));
        self.Sources = info.GetValue<IEnumerable<string>>(nameof(MFFTestDataBase.Sources));

        self.ExpectedDiagnostics = info.GetValue<object?[][]>(nameof(MFFTestDataBase.ExpectedDiagnostics))
            .Select(ed =>
            {
                if (ed is not null && ed.Length > 3)
                {
                    var diagnosticId = ed[0]?.ToString() ?? throw new NullReferenceException("ed[0] 'DiagnosticId'");
                    var diagnosticSeverityString = ed[1]?.ToString() ?? throw new NullReferenceException("ed[1] 'DiagnosticSeverity'");

                    var diagnosticResult = new DiagnosticResult(
                        diagnosticId,
#if NETSTANDARD2_0
                        ParseDiagnosticSeverity(diagnosticSeverityString));
#else
                        Enum.Parse<DiagnosticSeverity>(diagnosticSeverityString));
#endif
                    if (ed[2] is not null)
                    {
                        diagnosticResult = diagnosticResult.WithMessage(ed[2]!.ToString());
                    }

                    if (ed[3] is not null)
                    {
                        var spans = (ed[3] as object[]) ?? throw new NullReferenceException("ed[3] 'Spans'");
                        foreach (var span in spans)
                        {
                            var parts = span?.ToString()?.Split(',');
                            if (parts is not null && parts.Length > 4)
                            {
                                diagnosticResult = diagnosticResult.WithSpan(
                                    parts[0],
                                    int.Parse(parts[1]),
                                    int.Parse(parts[2]),
                                    int.Parse(parts[3]),
                                    int.Parse(parts[4]));
                            }
                            else
                            {
                                throw new Exception($"Unable to deserialize Spans. 'parts is null': {parts is null}, parts.Length: {(parts is null ? 0 : parts.Length)}");
                            }
                        }
                    }
                    return diagnosticResult;
                }
                else
                {
                    throw new Exception($"Unable to deserialize ExpectedDiagnostics. 'ed is null': {ed is null}, ed.Length: {(ed is null ? 0 : ed.Length)}");
                }
            }).ToArray();

        self.AdditionalFiles = info.GetValue<object[][]>(nameof(MFFTestDataBase.AdditionalFiles))
            .Select(af => (af[0].ToString() ?? string.Empty, af[1].ToString() ?? string.Empty)).ToArray();
        self.AdditionalReferences = info.GetValue<IEnumerable<string>>(nameof(MFFTestDataBase.AdditionalReferences))
            .Select(ar => Assembly.Load(ar));
    }

    public static void SerializeTestData(
        this MFFTestDataBase self,
        IXunitSerializationInfo info)
    {
        info.AddValue(nameof(MFFTestDataBase.Scenario), self.Scenario);
        info.AddValue(nameof(MFFTestDataBase.Sources), self.Sources);

        if (self.ExpectedDiagnostics is not null)
        {
            info.AddValue(nameof(MFFTestDataBase.ExpectedDiagnostics), self.ExpectedDiagnostics
                .Select(ed => new object?[] { ed.Id, ed.Severity.ToString(), ed.Message,
                ed.Spans.Select(s => $"{s.Span.Path}," +
                                $"{s.Span.StartLinePosition.Line + 1}," +
                                $"{s.Span.StartLinePosition.Character + 1}," +
                                $"{s.Span.EndLinePosition.Line + 1}," +
                                $"{s.Span.EndLinePosition.Character + 1}"
            ).ToArray() }).ToArray());
        }

        if (self.AdditionalFiles is not null)
        {
            info.AddValue(nameof(MFFTestDataBase.AdditionalFiles), self.AdditionalFiles
                .Select(af => new object[] { af.Item1, af.Item2 }).ToArray());
        }

        if (self.AdditionalReferences is not null)
        {
            info.AddValue(nameof(MFFTestDataBase.AdditionalReferences), self.AdditionalReferences
                .Select(ar => ar.FullName));
        }
    }

    public static void DeserializeAnalyzerTestData(
        this MFFAnalyzerTestData self,
        IXunitSerializationInfo info)
    {
        self.DeserializeTestData(info);
        self.Analyzers = info.GetValue<string[]>(nameof(MFFAnalyzerTestData.Analyzers))
            .Select(a =>
            {
                var type = Type.GetType(a, true);

                if (type is not null)
                {
                    var analyzer = Activator.CreateInstance(type) as DiagnosticAnalyzer;
                    if (analyzer is not null)
                    {
                        return analyzer;
                    }
                }

                throw new Exception($"Unable to create analyzer. type: '{a}'");
            }).ToArray();
    }

    public static void SerializeAnalyzerTestData(
        this MFFAnalyzerTestData self,
        IXunitSerializationInfo info)
    {
        self.SerializeTestData(info);
        info.AddValue(nameof(MFFAnalyzerTestData.Analyzers), self.Analyzers
            .Select(a => a.GetType().AssemblyQualifiedName).ToArray());
    }

}
