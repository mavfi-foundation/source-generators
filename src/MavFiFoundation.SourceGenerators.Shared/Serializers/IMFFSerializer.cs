
namespace MavFiFoundation.SourceGenerators.Serializers;

public interface IMFFSerializer
{
    string SerializeObject(object value);

    object? DeserializeObject(string value, Type type);

    T? DeserializeObject<T>(string value);

    void RegisterSerializerFor<T>(Func<object, string> serializer);

    void RegisterSerializerFor(Func<object, string> serializer, Type type);

    void RegisterDeserializerFor<T>(Func<string, object?> deserializer);

    void RegisterDeserializerFor(Func<string, object?> deserializer, Type type);
}
