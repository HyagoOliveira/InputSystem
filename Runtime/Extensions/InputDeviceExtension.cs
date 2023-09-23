using UnityEngine.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Extension class for <see cref="InputDevice"/>.
    /// </summary>
    public static class InputDeviceExtension
    {
        /// <summary>
        /// Returns the type for an <see cref="InputDevice"/>.
        /// <para>
        /// <b>Note</b>: only WebGL supports difference between 
        /// <see cref="InputDeviceType.XboxSystem"/> and <see cref="InputDeviceType.Xbox360"/>.
        /// </para>
        /// </summary>
        /// <param name="device"></param>
        /// <returns>A <see cref="InputDeviceType"/> value.</returns>
        public static InputDeviceType GetInputDeviceType(this InputDevice device)
        {
            var type = InputDeviceType.NotFound;

            if (device is Keyboard || device is Mouse) type = InputDeviceType.KeyboardAndMouse;
            else if (device is UnityEngine.InputSystem.XInput.XInputController) type = InputDeviceType.XboxSystem;
            else if (device is UnityEngine.InputSystem.DualShock.DualShockGamepad) type = InputDeviceType.PlaystationSystem;
            else if (device is Gamepad) type = InputDeviceType.GenericGamepad;

#if UNITY_EDITOR || UNITY_ANDROID
            if (device is UnityEngine.InputSystem.Android.AndroidGamepad) type = InputDeviceType.AndroidGamepad;
            else if (device is UnityEngine.InputSystem.Android.XboxOneGamepadAndroid) type = InputDeviceType.XboxOne;
#endif

#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WSA || UNITY_PS3 || UNITY_PS4 || UNITY_PS5 || UNITY_SWITCH
            if (device is UnityEngine.InputSystem.DualShock.DualShock3GamepadHID) type = InputDeviceType.Playstation3;
            else if (device is UnityEngine.InputSystem.DualShock.DualShock4GamepadHID) type = InputDeviceType.Playstation4;
            else if (device is UnityEngine.InputSystem.DualShock.DualSenseGamepadHID) type = InputDeviceType.Playstation5;
            else if (device is UnityEngine.InputSystem.Switch.SwitchProControllerHID) type = InputDeviceType.SwitchProController;
#endif

#if UNITY_EDITOR || UNITY_IOS || UNITY_TVOS || UNITY_PS4
            if (device is UnityEngine.InputSystem.iOS.DualShock4GampadiOS) type = InputDeviceType.Playstation4;
            if (device is UnityEngine.InputSystem.iOS.DualSenseGampadiOS) type = InputDeviceType.Playstation5;
            else if (device is UnityEngine.InputSystem.iOS.XboxOneGampadiOS) type = InputDeviceType.XboxOne;
            else if (device is UnityEngine.InputSystem.iOS.iOSGameController) type = InputDeviceType.iOSGamepad;
#endif

#if UNITY_WEBGL
            if (device is UnityEngine.InputSystem.WebGL.WebGLGamepad)
            {
                var description = device.description.product.ToLower();

                if (description.Contains("xbox 360")) type = InputDeviceType.Xbox360;
                else if (description.Contains("xbox")) type = InputDeviceType.XboxSystem;
                else if (description.Contains("dualsense")) type = InputDeviceType.Playstation5;
            }
#endif
            return type;
        }
    }
}