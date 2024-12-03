using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace SIT.Shared.Extensions;

public static class JsonExtensions
{
    private static readonly Lazy<JsonSerializerOptions> LazyOptions = new(() => new JsonSerializerOptions().ConfigureJsonOptions(),
        isThreadSafe: true);

    public static T FromJson<T>(this string value)
    {
        return value != null
            ? JsonSerializer.Deserialize<T>(value, LazyOptions.Value)
            : default;
    }
    
    public static string ToJson<T>(this T value)
    {
        return !value.IsDefault() 
            ? JsonSerializer.Serialize(value, LazyOptions.Value) 
            : default;
    }

    public static JsonSerializerOptions ConfigureJsonOptions(this JsonSerializerOptions settings)
    {
        settings.WriteIndented = false;
        settings.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        settings.ReadCommentHandling = JsonCommentHandling.Skip;
        settings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        settings.TypeInfoResolver = new InternalConstructorContractResolver();
        settings.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

        return settings;
    }
}

internal sealed class InternalConstructorContractResolver : DefaultJsonTypeInfoResolver
{
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        var jsonTypeInfo = base.GetTypeInfo(type, options);

        if (jsonTypeInfo.Kind == JsonTypeInfoKind.Object
            && jsonTypeInfo.CreateObject is null
            && jsonTypeInfo.Type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Length == 0)
        {
            jsonTypeInfo.CreateObject = () => Activator.CreateInstance(jsonTypeInfo.Type, true);
        }
        
        return jsonTypeInfo;
    }
}