using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using ProcGen;

namespace MyMods;

[HarmonyPatch(typeof(SettingsCache), "GetRandomTraits")]
public class SettingsCache_SortRandomTraits
{
    private static void Postfix(ref List<string> __result)
        => __result = __result.OrderBy(x => x).ToList();
}
