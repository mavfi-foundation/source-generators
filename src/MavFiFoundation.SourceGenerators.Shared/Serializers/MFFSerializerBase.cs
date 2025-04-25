
namespace MavFiFoundation.SourceGenerators.Serializers;

public abstract class MFFSerializerBase : IMFFSerializer
{
    protected IDictionary<Type, Func<object, string>> Serializers {get; private set;}
    protected IDictionary<Type, Func<string, object?>> Deserializers {get; private set;} 

    public MFFSerializerBase()
    {
        Serializers = new Dictionary<Type, Func<object, string>>();
        Deserializers = new Dictionary<Type, Func<string, object?>>();
    }

    public void RegisterSerializerFor<T>(Func<object, string> serializer)
    {
        RegisterSerializerFor(serializer, typeof(T));
    }

    public void RegisterSerializerFor(Func<object, string> serializer, Type type)
    {
        Serializers.Add(type, serializer);
    }

    public void RegisterDeserializerFor<T>(Func<string, object?> deserializer)
    {
        RegisterDeserializerFor(deserializer, typeof(T));
    }

    public void RegisterDeserializerFor(Func<string, object?> deserializer, Type type)
    {
        Deserializers.Add(type, deserializer);
    }

    public string SerializeObject(object value)
    {
        Type type = value.GetType();

        if(Serializers.ContainsKey(type))
        {
            return Serializers[type].Invoke(value);
        }
        else
        {
            return AbsSerializeObject(value);
        }
    }

    public object? DeserializeObject(string value, Type type)
    {
        if(Deserializers.ContainsKey(type))
        {
            return Deserializers[type].Invoke(value);
        }
        else
        {
            return AbsDeserializeObject(value, type);
        }
    }

    public T? DeserializeObject<T>(string value)
    {
        return (T?)DeserializeObject(value, typeof(T));
    }

    protected abstract string AbsSerializeObject(object value);

    protected abstract object? AbsDeserializeObject(string value, Type type);
 
}
