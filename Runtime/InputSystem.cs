using System;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace ActionCode.InputSystem
{
    public static class InputSystem
    {
        /// <summary>
        /// Event fired when any input from any connected device changed.
        /// <para>
        /// Suppose you have a keyboard and gamepad connected and you are currently using the keyboard. 
        /// This event is fired once any button is pressed on the gamepad or any movement is done in its stick.<br/>
        /// The event will be fired again once an input is done on the keyboard or mouse is moved.
        /// </para>
        /// </summary>
        public static event Action<InputDeviceType> OnDeviceInputChanged
        {
            add
            {
                var shouldBind = !ContainsAnyBinder();
                InternalOnDeviceInputChanged += value;

                if (shouldBind) BindIntoInputOnEvent();
            }

            remove
            {
                InternalOnDeviceInputChanged -= value;
                if (!ContainsAnyBinder()) UnbindFromInputOnEvent();
            }
        }

        private static InputDeviceType lastDeviceType = InputDeviceType.None;
        private static event Action<InputDeviceType> InternalOnDeviceInputChanged;

        private static bool ContainsAnyBinder() => InternalOnDeviceInputChanged?.GetInvocationList().Length > 0;
        private static void BindIntoInputOnEvent() => UnityEngine.InputSystem.InputSystem.onEvent += HandleInputEvent;
        private static void UnbindFromInputOnEvent() => UnityEngine.InputSystem.InputSystem.onEvent -= HandleInputEvent;

        private static void HandleInputEvent(InputEventPtr eventPtr, InputDevice control)
        {
            // Ignore anything that isn't a state event.
            var isStateEvent = !eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>();
            if (isStateEvent) return;

            var device = control.device;
            var isSupportedDevice = device is Gamepad || device is Keyboard || device is Mouse;
            if (!isSupportedDevice) return;

            //var isValidMouseAction = device is Mouse mouse && mouse.delta.value.sqrMagnitude > 0.5f;
            //if (!isValidMouseAction) return;

            var deviceType = device.GetInputDeviceType();
            if (deviceType == lastDeviceType) return;

            // Some devices span multiple events like PS4 and PS5 controllers on PC.
            var isSpanningEvents = !eventPtr.EnumerateChangedControls(device, magnitudeThreshold: 0.0001f).Any();
            if (isSpanningEvents) return;

            lastDeviceType = deviceType;
            InternalOnDeviceInputChanged?.Invoke(lastDeviceType);
        }
    }
}