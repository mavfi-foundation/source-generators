using MavFiFoundation.SourceGenerators.Serializers;

namespace MavFiFoundation.SourceGenerators.GeneratorTriggers;

public class MFFXmlGeneratorTrigger : MFFFileGeneratorTriggerBase
{
    public const string DEFAULT_NAME = nameof(MFFXmlGeneratorTrigger);
    public const string DEFAULT_FILE_NAME_SUFFIX = ".CodeGen.xml";
    public MFFXmlGeneratorTrigger(IMFFSerializer serializer) : base(
        DEFAULT_NAME,
        DEFAULT_FILE_NAME_SUFFIX,
        serializer) { }
}
