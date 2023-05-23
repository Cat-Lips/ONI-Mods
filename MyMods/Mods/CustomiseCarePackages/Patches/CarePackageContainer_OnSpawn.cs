using System;
using HarmonyLib;

namespace MyMods;

[HarmonyPatch(typeof(CarePackageContainer), "OnSpawn")]
public class CarePackageContainer_OnSpawn
{
    public static event Action<CarePackageContainer> OnPostfix;

    private static void Postfix(CarePackageContainer __instance)
        => OnPostfix?.Invoke(__instance);
}
