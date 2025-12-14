using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Invokes <see cref="OnAnyButtonPressed"/> event when any button is pressed.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class AnyButtonPressedListener : MonoBehaviour
    {
        [Min(0f), Tooltip("Time (in seconds) to wait before listen for any input.")]
        public float waitingTime = 1f;
        [Tooltip("If enabled, it will disable this component after OnAnyButtonPressed event is fired.")]
        public bool disableAfterEvent = true;

        [Space]
        [Tooltip("Event fired when any button is pressed once.")]
        public UnityEvent OnAnyButtonPressed;

        private IDisposable anyButtonListener;

        private void OnEnable() => Invoke(nameof(StartListenForAnyButtonPress), waitingTime);
        private void OnDisable() => anyButtonListener?.Dispose();

        private void StartListenForAnyButtonPress()
        {
            var pressEvent = UnityEngine.InputSystem.InputSystem.onAnyButtonPress;
            anyButtonListener = pressEvent.Call(HandleAnyButtonPressed);
        }

        private void HandleAnyButtonPressed(InputControl input)
        {
            if (!IsValidDevicePress(input.device)) return;

            OnAnyButtonPressed?.Invoke();
            if (disableAfterEvent) enabled = false;
        }

        private static bool IsValidDevicePress(InputDevice device) => device is not Mouse mouse || mouse.IsInsideGameView();
    }
}