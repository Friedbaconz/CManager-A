using System.Text.Json;

namespace CManager.Infrastructure.ConsoleApp.Serilization;

public static class JsonDataFormatter
{
    private static readonly JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true,

    };

    public static string Serialize<T>(T content) => JsonSerializer.Serialize(content, options);

    public static T? Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, options) ?? default;
}
