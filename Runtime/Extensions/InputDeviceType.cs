namespace ActionCode.InputSystem
{
    /// <summary>
    /// Types for inputs devices.
    /// </summary>
    public enum InputDeviceType
    {
        None = -1,
        GenericGamepad = 0,
        WebGLGamepad,
        AndroidGamepad,
        iOSGamepad,
        KeyboardAndMouse,

        PlaystationSystem = 10,
        Playstation3,
        Playstation4,
        Playstation5,

        XboxSystem = 20,
        Xbox360,            // Only available for WebGL builds. Otherwise it'll be a XboxSystem.
        XboxOne,
        XboxSeriesX,

        NintendoSystem = 30,
        SwitchProController,

        NotFound = 99
    }
}