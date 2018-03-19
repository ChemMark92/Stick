public static class Distances
{
    #region Fields
    const float deltaCenter = 0.1f;
    #endregion


    #region Properties
    public static float DeltaCenter {
        get
        {
            return deltaCenter;
        }
    }
    public static float Space { get; set; }
    public static float RightPlatformWidth { get; set; }
    public static float LeftPlatformWidth { get; set; }
    public static float StickLength { get; set; }
    public static float CharacterRunDistance { get; set; }
    public static float CharacterRunOnStickDistance { get; set; }
    #endregion
}
