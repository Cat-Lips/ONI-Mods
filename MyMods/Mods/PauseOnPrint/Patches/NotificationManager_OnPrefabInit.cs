using System;
using HarmonyLib;

namespace MyMods;

[HarmonyPatch(typeof(NotificationManager), "OnPrefabInit")]
public class NotificationManager_OnPrefabInit
{
    public static event Action<NotificationManager> OnPostfix;

    private static void Postfix(NotificationManager __instance)
        => OnPostfix?.Invoke(__instance);
}
