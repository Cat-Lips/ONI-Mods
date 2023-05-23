using System;
using HarmonyLib;

namespace MyMods;

[HarmonyPatch(typeof(CharacterContainer), "OnSpawn")]
public class CharacterContainer_OnSpawn
{
    public static event Action<CharacterContainer> OnPostfix;

    private static void Postfix(CharacterContainer __instance)
        => OnPostfix?.Invoke(__instance);
}
