using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
//TODO: Use System.Text.Json
namespace MavFiFoundation.SourceGenerators.Serializers;

public class MFFJsonSerializer : MFFSerializerBase, IMFFSerializer
{

    public MFFJsonSerializer()
    {
        
    }

    protected override object? AbsDeserializeObject(string value, Type type)
    {
        return JsonConvert.DeserializeObject(value, type);
    }

    protected override string AbsSerializeObject(object value)
    {
        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };

        var jsonSettings = new JsonSerializerSettings()
        {
            ContractResolver = contractResolver,
            Formatting = Formatting.Indented,
        };

        return JsonConvert.SerializeObject(value, jsonSettings);
    }
}
