using System.Linq;

namespace MyMods;

public static class KleiExtensions
{
    public static bool IsDlc3(this IHasDlcRestrictions source)
        => source.GetRequiredDlcIds()?.Contains(DlcManager.DLC3_ID) ?? false;
}
