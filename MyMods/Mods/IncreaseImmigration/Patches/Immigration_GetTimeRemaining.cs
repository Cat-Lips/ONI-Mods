using System;
using HarmonyLib;

namespace MyMods;

[HarmonyPatch(typeof(Immigration), "GetTimeRemaining")]
public class Immigration_GetTimeRemaining
{
    public static event Action<Immigration> OnPrefix;

    private static void Prefix(Immigration __instance)
        => OnPrefix?.Invoke(__instance);
}
