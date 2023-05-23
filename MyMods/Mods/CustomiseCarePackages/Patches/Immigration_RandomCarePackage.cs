using HarmonyLib;

namespace MyMods;

[HarmonyPatch(typeof(Immigration), "RandomCarePackage")]
public class Immigration_RandomCarePackage
{
    public static CarePackageInfo Override;

    private static bool Prefix(ref CarePackageInfo __result)
    {
        if (Override is not null)
        {
            __result = Override;
            return false; // Do not run
        }

        return true; // RandomCarePackage
    }
}
