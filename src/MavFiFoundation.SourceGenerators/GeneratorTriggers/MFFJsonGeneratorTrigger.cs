
using MavFiFoundation.SourceGenerators.Serializers;

namespace MavFiFoundation.SourceGenerators.GeneratorTriggers;

public class MFFJsonGeneratorTrigger : MFFFileGeneratorTriggerBase
{
    public const string DEFAULT_NAME = nameof(MFFJsonGeneratorTrigger);

    public const string DEFAULT_FILE_NAME_SUFFIX = ".CodeGen.json";
    public MFFJsonGeneratorTrigger(IMFFSerializer serializer) : base(
        DEFAULT_NAME,
        DEFAULT_FILE_NAME_SUFFIX,
        serializer) { }
}
