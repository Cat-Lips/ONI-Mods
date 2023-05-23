using HarmonyLib;
using ProcGenGame;

namespace MyMods;

[HarmonyPatch(typeof(WorldGen), "Initialise")]
public class WorldGen_Initialise
{
    private static void Prefix(ref bool debug)
        => debug = OnWorldGenError.Continue;
}
