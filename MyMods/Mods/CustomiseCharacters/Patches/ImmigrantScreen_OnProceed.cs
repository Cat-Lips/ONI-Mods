using System;
using HarmonyLib;

namespace MyMods;

[HarmonyPatch(typeof(ImmigrantScreen), "OnProceed")]
public class ImmigrantScreen_OnProceed
{
    public static event Action<ImmigrantScreen> OnPostfix;

    private static void Postfix(ImmigrantScreen __instance)
        => OnPostfix?.Invoke(__instance);
}
