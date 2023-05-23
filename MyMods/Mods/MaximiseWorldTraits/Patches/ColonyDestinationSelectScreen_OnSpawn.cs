using System;
using HarmonyLib;

namespace MyMods;

[HarmonyPatch(typeof(ColonyDestinationSelectScreen), "OnSpawn")]
public class ColonyDestinationSelectScreen_OnSpawn
{
    public static event Action<ColonyDestinationSelectScreen> OnPostfix;

    private static void Postfix(ColonyDestinationSelectScreen __instance)
        => OnPostfix?.Invoke(__instance);
}
