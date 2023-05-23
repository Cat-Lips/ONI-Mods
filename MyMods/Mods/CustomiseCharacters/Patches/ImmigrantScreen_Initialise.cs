using System;
using HarmonyLib;

namespace MyMods;

[HarmonyPatch(typeof(ImmigrantScreen), "InitializeImmigrantScreen")]
public class ImmigrantScreen_Initialise
{
    public static event Action<ImmigrantScreen> OnPostfix;

    private static void Postfix()
        => OnPostfix?.Invoke(ImmigrantScreen.instance);
}
