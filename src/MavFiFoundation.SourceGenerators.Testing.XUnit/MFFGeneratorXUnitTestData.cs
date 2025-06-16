// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using Xunit.Abstractions;

namespace MavFiFoundation.SourceGenerators.Testing;

public class MFFGeneratorXUnitTestData: MFFGeneratorTestData, IXunitSerializable
{
    public void Deserialize(IXunitSerializationInfo info)
    {
        this.DeserializeTestData(info);
        GeneratedSources = info.GetValue<object[][]>(nameof(GeneratedSources))
            .Select(gs => ((Type)gs[0], gs[1].ToString() ?? string.Empty, gs[2].ToString() ?? string.Empty)).ToArray();
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        this.SerializeTestData(info);
        info.AddValue(nameof(GeneratedSources), GeneratedSources
            .Select(gs => new object[] {gs.Item1, gs.Item2, gs.Item3}).ToArray());
    }
}
