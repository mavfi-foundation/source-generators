// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Xunit.Abstractions;

namespace MavFiFoundation.SourceGenerators.Testing;

public class MFFCodeFixXUnitTestData: MFFCodeFixTestData, IXunitSerializable
{
    public void Deserialize(IXunitSerializationInfo info)
    {
        this.DeserializeAnalyzerTestData(info);

        CodeFixProviders = info.GetValue<string[]>(nameof(CodeFixProviders))
            .Select(cf =>
            {
                var type = Type.GetType(cf, true);

                if (type is not null)
                {
                    var codeFix = Activator.CreateInstance(type) as CodeFixProvider;
                    if (codeFix is not null)
                    {
                        return codeFix;
                    }
                }

                throw new Exception($"Unable to create code fix. type: '{cf}'");
            }).ToArray();

        FixedSource = info.GetValue<string>(nameof(FixedSource));
        CodeActionIndex = info.GetValue<int?>(nameof(CodeActionIndex));
        NumberOfIncrementalIterations = info.GetValue<int?>(nameof(NumberOfIncrementalIterations));
        CodeActionEquivalenceKey = info.GetValue<string?>(nameof(CodeActionEquivalenceKey));
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        this.SerializeAnalyzerTestData(info);

        info.AddValue(nameof(CodeFixProviders), CodeFixProviders
            .Select(cf => cf.GetType().AssemblyQualifiedName).ToArray());
        info.AddValue(nameof(FixedSource), FixedSource);
        info.AddValue(nameof(CodeActionIndex), CodeActionIndex);
        info.AddValue(nameof(NumberOfIncrementalIterations), NumberOfIncrementalIterations);
        info.AddValue(nameof(CodeActionEquivalenceKey), CodeActionEquivalenceKey);
    }
}
