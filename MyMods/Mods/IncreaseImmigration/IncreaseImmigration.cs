namespace MyMods;

public static class IncreaseImmigration
{
    private const float CycleTime = 600f;
    public static int SpawnCycles { get; set; } = 1;
    public static float SpawnInterval => CycleTime * SpawnCycles;

    public static void Initialise()
    {
        Immigration_OnPrefabInit.OnPrefix += OnPrefabInit;
        Immigration_GetTimeRemaining.OnPrefix += OnGetTimeRemaining;

        static void OnPrefabInit(Immigration instance)
            => instance.spawnInterval = [SpawnInterval];

        static void OnGetTimeRemaining(Immigration instance)
        {
            if (instance.timeBeforeSpawn > SpawnInterval)
                instance.timeBeforeSpawn = SpawnInterval;
        }
    }
}
