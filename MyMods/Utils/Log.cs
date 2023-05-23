using System.IO;
using System.Runtime.CompilerServices;

namespace MyMods;

public static class Log
{
    public static bool EnableFileName { get; set; } = true;
    public static bool EnableMemberName { get; set; } = false;

    //[Conditional("DEBUG")]
    public static void Dev(object msg = null, [CallerFilePath] string filePath = null, [CallerMemberName] string memberName = null) => Debug.Log(Format(filePath, memberName, msg));
    public static void Info(object msg = null, [CallerFilePath] string filePath = null, [CallerMemberName] string memberName = null) => Debug.Log(Format(filePath, memberName, msg));
    public static void Warn(object msg = null, [CallerFilePath] string filePath = null, [CallerMemberName] string memberName = null) => Debug.LogWarning(Format(filePath, memberName, msg));
    public static void Error(object msg = null, [CallerFilePath] string filePath = null, [CallerMemberName] string memberName = null) => Debug.LogError(Format(filePath, memberName, msg));

    private static string Format(string filePath, string memberName, object msg)
        => $"[{Bootstrap.Title}] {FileName(filePath)}{MemberName(memberName)}{msg}";

    private static string FileName(string x) => EnableFileName ? $"[{Path.GetFileNameWithoutExtension(x)}] " : null;
    private static string MemberName(string x) => EnableMemberName && x is not null ? $"[{x}] " : null;
}
