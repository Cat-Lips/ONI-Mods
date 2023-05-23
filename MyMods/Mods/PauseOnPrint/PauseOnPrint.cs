using System.Collections.Generic;

namespace MyMods;

public static class PauseOnPrint
{
    private static IEnumerable<StatusItem> Items
    {
        get
        {
            yield return Db.Get().BuildingStatusItems.NoResearchSelected;
            yield return Db.Get().BuildingStatusItems.NewDuplicantsAvailable;
        }
    }

    public static void Initialise()
    {
        NotificationManager_OnPrefabInit.OnPostfix += InitNotify;

        static void InitNotify(NotificationManager source)
        {
            var items = new HashSet<StatusItem>(Items);
            source.notificationAdded += OnNotify;

            void OnNotify(Notification source)
            {
                //Log.Dev($"*** Notification [Type: {source.GetType().Name}, Data1: {source.tooltipData?.GetType().Name}, Data2: {source.customClickData?.GetType().Name}, NotifierName: {source.NotifierName}, TitleText: {source.titleText}]");

                if (source.tooltipData is StatusItem item && items.Contains(item))
                {
                    if (!SpeedControlScreen.Instance.IsPaused)
                        SpeedControlScreen.Instance.Pause();
                }
            }
        }
    }
}
