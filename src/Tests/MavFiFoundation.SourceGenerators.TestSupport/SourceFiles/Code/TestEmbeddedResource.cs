// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using MavFiFoundation.SourceGenerators;

namespace TestSpace;

[MFFEmbeddedResource]
public static class TestEmbeddedResource
{
    public const string Resource = 
"""
#nullable enable

public partial class {{ srcType.Name }}_Generated
{
{{- for field in srcType.Fields }}

    {{ field.DeclaredAccessibilty | string.downcase }} {{ field.TypeFullyQualifiedName }} {{ field.Name }};
{{- end }}
{{- for property in srcType.Properties }}

    {{ property.DeclaredAccessibilty | string.downcase }} {{ property.TypeFullyQualifiedName }} {{property.Name }} {
        {{- if property.GetMethod }} get;{{ end }}{{ if property.GetMethod }} set;{{ end }} }
{{- end }}
{{- for method in srcType.Methods }}
    {{- if !method.IsInherited && method.Name != ".ctor"}}

    {{ method.DeclaredAccessibilty | string.downcase }} {{ method.TypeFullyQualifiedName }} {{ method.Name }} (
        {{- for parameter in method.Parameters  }}
            {{- parameter.TypeFullyQualifiedName }} {{ parameter.Name }}{{ if !for.last }}, {{ end }}
        {{- end }})
    {
        return false;
    }
    {{- end }}
{{- end }}
}
""";
}
