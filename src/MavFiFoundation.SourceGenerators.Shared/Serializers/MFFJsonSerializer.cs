using System.Text.Json;
using MavFiFoundation.SourceGenerators.Serializers.Json;

namespace MavFiFoundation.SourceGenerators.Serializers;

public class MFFJsonSerializer : MFFSerializerBase, IMFFSerializer
{
    private readonly JsonSerializerOptions _jsonOptions;
    public MFFJsonSerializer()
    {
        _jsonOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        _jsonOptions.Converters.Add(new ObjectToInferredTypesConverter());
    }

    protected override object? AbsDeserializeObject(string value, Type type)
    {
        return JsonSerializer.Deserialize(value, type, _jsonOptions);
    }

    protected override string AbsSerializeObject(object value)
    {
        return JsonSerializer.Serialize(value, _jsonOptions);
    }
}
