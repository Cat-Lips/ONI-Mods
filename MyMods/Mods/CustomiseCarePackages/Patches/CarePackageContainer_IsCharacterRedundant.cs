using HarmonyLib;

namespace MyMods;

[HarmonyPatch(typeof(CarePackageContainer), "IsCharacterRedundant")]
public class CarePackageContainer_IsCharacterRedundant
{
    public static bool Override;

    private static bool Prefix(ref bool __result)
    {
        if (Override)
        {
            __result = false;
            return false; // Do not run
        }

        return true; // IsCharacterRedundant
    }
}
