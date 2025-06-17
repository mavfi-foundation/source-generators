// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

using MavFiFoundation.SourceGenerators.GeneratorTriggers;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.ResourceLoaders;
using MavFiFoundation.SourceGenerators.Serializers;

using System.Collections.Immutable;

namespace MavFiFoundation.SourceGenerators.UnitTests.GeneratorTriggers;

public class MFFFileGeneratorTriggerBaseTestClass : MFFFileGeneratorTriggerBase
{
    public const string TestName = "TestFileGenerator";
    public const string TestFileNameSuffix = ".CodeGen.test";

    public MFFFileGeneratorTriggerBaseTestClass(IMFFSerializer serializer)
        : base(TestName, TestFileNameSuffix, serializer)
    {

    }

    public MFFGeneratorInfoRecord? Exposed_GetGeneratorInfoFromFile(
        IEnumerable<IMFFResourceLoader> resourceLoaders,
        MFFResourceRecord resource,
        ImmutableArray<MFFResourceRecord> resources,
        CancellationToken cancellationToken)
    {
        return base.GetGeneratorInfoFromFile(resourceLoaders, resource, resources, cancellationToken);
    }
}
