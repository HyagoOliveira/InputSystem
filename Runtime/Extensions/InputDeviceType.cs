namespace ActionCode.InputSystem
{
    /// <summary>
    /// Types for inputs devices.
    /// </summary>
    public enum InputDeviceType
    {
        None = 0,
        KeyboardAndMouse,
        AndroidGamepad,
        iOSGamepad,
        GenericGamepad,
        PlaystationSystem,
        Playstation3,
        Playstation4,
        SwitchProController,
        XboxSystem,
        WebGLGamepad,
        NotFound = 99
    }
}