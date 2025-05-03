using MavFiFoundation.SourceGenerators.GeneratorTriggers;
using MavFiFoundation.SourceGenerators.Models;
using MavFiFoundation.SourceGenerators.ResourceLoaders;
using MavFiFoundation.SourceGenerators.Serializers;
using System.Collections.Immutable;

namespace MavFiFoundation.SourceGenerators.UnitTests.GeneratorTriggers;

public class MFFFileGeneratorTriggerBaseTestClass : MFFFileGeneratorTriggerBase
{
    public const string TEST_NAME = "TestFileGenerator";
    public const string TEST_FILE_NAME_SUFFIX = ".CodeGen.test";

    public MFFFileGeneratorTriggerBaseTestClass(IMFFSerializer serializer)
        :base(TEST_NAME, TEST_FILE_NAME_SUFFIX, serializer)
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
