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

}
""";
}
