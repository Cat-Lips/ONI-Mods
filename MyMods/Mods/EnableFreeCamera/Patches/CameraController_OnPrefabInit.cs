using System;
using HarmonyLib;

namespace MyMods;

[HarmonyPatch(typeof(CameraController), "OnPrefabInit")]
public class CameraController_OnPrefabInit
{
    public static event Action<CameraController> OnPostfix;

    private static void Postfix(CameraController __instance)
        => OnPostfix?.Invoke(__instance);
}
