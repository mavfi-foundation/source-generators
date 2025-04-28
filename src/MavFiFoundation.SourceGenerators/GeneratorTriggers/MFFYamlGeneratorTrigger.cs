
using MavFiFoundation.SourceGenerators.Serializers;

namespace MavFiFoundation.SourceGenerators.GeneratorTriggers;

public class MFFYamlGeneratorTrigger : MFFFileGeneratorTriggerBase
{
    public const string DEFAULT_NAME = nameof(MFFYamlGeneratorTrigger);

    public const string DEFAULT_FILE_NAME_SUFFIX = ".CodeGen.yml";
    public MFFYamlGeneratorTrigger(IMFFSerializer serializer) : base(
        DEFAULT_NAME,
        DEFAULT_FILE_NAME_SUFFIX,
        serializer) { }
}
