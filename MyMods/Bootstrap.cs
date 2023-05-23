using HarmonyLib;
using KMod;

namespace MyMods;

public class Bootstrap : UserMod2
{
    public static string Path { get; private set; }
    public static string Title { get; private set; }

    public override void OnLoad(Harmony harmony)
    {
        Path = path;
        Title = mod.title;
        base.OnLoad(harmony);

        InitialiseMod();

        static void InitialiseMod()
        {
            PauseOnPrint.Initialise();
            EnableFreeCamera.Initialise();
            MaximiseWorldTraits.Initialise();
            CustomiseCharacters.Initialise();
            IncreaseImmigration.Initialise();
            CustomiseCarePackages.Initialise();
        }
    }
}
