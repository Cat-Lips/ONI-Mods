using System.IO;
using Newtonsoft.Json;

namespace MyMods;

public static class Settings
{
    public static void Save<T>(T config, string name)
    {
        var path = GetPath(name);

        Log.Dev($"Saving {typeof(T).Name} to {path}");

        var json = JsonConvert.SerializeObject(config);
        File.WriteAllText(path, json);
    }

    public static T Load<T>(string name)
    {
        var path = GetPath(name);
        if (!File.Exists(path)) return default;

        Log.Dev($"Loading {typeof(T).Name} from {path}");

        var json = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<T>(json);
    }

    public static T Load<T>() => Load<T>($"{typeof(T).Name}.json");
    public static void Save<T>(T config) => Save(config, $"{typeof(T).Name}.json");

    private static string GetPath(string name)
        => Path.Combine(Bootstrap.Path, name);
}
