using System;
using HarmonyLib;

namespace MyMods;

[HarmonyPatch(typeof(Immigration), "OnPrefabInit")]
public class Immigration_OnPrefabInit
{
    public static event Action<Immigration> OnPrefix;

    private static void Prefix(Immigration __instance)
        => OnPrefix?.Invoke(__instance);
}
