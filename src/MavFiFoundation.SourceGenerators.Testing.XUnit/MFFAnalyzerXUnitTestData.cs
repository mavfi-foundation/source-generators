// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using Microsoft.CodeAnalysis.Diagnostics;
using Xunit.Abstractions;

namespace MavFiFoundation.SourceGenerators.Testing;

public class MFFAnalyzerXUnitTestData: MFFAnalyzerTestData, IXunitSerializable
{
    public void Deserialize(IXunitSerializationInfo info)
    {
        this.DeserializeAnalyzerTestData(info);
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        this.SerializeAnalyzerTestData(info);
    }
}
