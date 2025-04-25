using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace MavFiFoundation.SourceGenerators.Serializers;

public class MFFYamlSerializer : MFFSerializerBase, IMFFSerializer
{
    protected override object? AbsDeserializeObject(string value, Type type)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        
        return deserializer.Deserialize(value, type);
    }

    protected override string AbsSerializeObject(object value)
    {
        var serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        return serializer.Serialize(value);
    }
}
