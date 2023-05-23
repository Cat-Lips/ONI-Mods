using System;
using HarmonyLib;

namespace MyMods;

[HarmonyPatch(typeof(MinionSelectScreen), "OnPrefabInit")]
public class MinionSelectScreen_Initialise
{
    public static event Action<MinionSelectScreen> OnPostfix;

    private static void Postfix(MinionSelectScreen __instance)
        => OnPostfix?.Invoke(__instance);
}
