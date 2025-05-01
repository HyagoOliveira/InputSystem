using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Invokes <see cref="OnAnyButtonPressed"/> event when any button is pressed once.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class AnyButtonPressedListener : MonoBehaviour
    {
        /// <summary>
        /// Event fired when any button is pressed once.
        /// </summary>
        public UnityEvent OnAnyButtonPressed;

        private IDisposable anyButtonListener;

        private void OnEnable() => anyButtonListener = UnityEngine.InputSystem.InputSystem.onAnyButtonPress.CallOnce(HandleAnyButtonPressed);
        private void OnDisable() => anyButtonListener?.Dispose();

        private void HandleAnyButtonPressed(InputControl _) => OnAnyButtonPressed?.Invoke();
    }
}