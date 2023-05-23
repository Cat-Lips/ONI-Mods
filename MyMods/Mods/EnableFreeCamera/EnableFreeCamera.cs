namespace MyMods;

public static class EnableFreeCamera
{
    public static void Initialise()
    {
        CameraController_OnPrefabInit.OnPostfix += OnPrefabInit;

        static void OnPrefabInit(CameraController camera)
            => camera.FreeCameraEnabled = true;
    }
}
