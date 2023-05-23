using System;
using HarmonyLib;

namespace MyMods;

[HarmonyPatch(typeof(MinionSelectScreen), "OnProceed")]
public class MinionSelectScreen_OnProceed
{
    public static event Action<MinionSelectScreen> OnPostfix;

    private static void Postfix(MinionSelectScreen __instance)
        => OnPostfix?.Invoke(__instance);
}
