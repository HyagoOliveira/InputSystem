using System;
using UnityEngine;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Disables the mouse cursor when a Gamepad is used and enables it again when Keyboard/Mouse is used.<br/>
    /// When the mouse is disabled, its position is warped to the Screen top-left corner so it do not stay over any Selectable UI.
    /// </summary>
    /// <remarks>
    /// On Editor, if the Mouse is outside the GameView, the cursor will be set to visible regardless 
    /// if using Gamepad or not.
    /// </remarks>
    [DisallowMultipleComponent]
    public sealed class DisableCursorListener : MonoBehaviour
    {
        /// <summary>
        /// Event fired when Mouse visibility changed.
        /// </summary>
        public static event Action<bool> OnVisibilityChanged;

        private void OnEnable() => InputSystem.OnDeviceInputChanged += HandleDeviceInputChanged;
        private void OnDisable() => InputSystem.OnDeviceInputChanged -= HandleDeviceInputChanged;

        private static void HandleDeviceInputChanged(InputDeviceType type) => SetCursorVisibility(type);

        private static void SetCursorVisibility(InputDeviceType type)
        {
            var isVisible = type == InputDeviceType.KeyboardAndMouse;
            SetCursorVisibility(isVisible);
        }

        /// <summary>
        /// Sets the cursor visibility.
        /// <para>
        /// If mouse is outside GameView, the cursor will be set to visible regardless of the given parameter.
        /// </para>
        /// </summary>
        /// <param name="isVisible">Whether mouse cursor is visible.</param>
        public static async void SetCursorVisibility(bool isVisible)
        {
            if (!isVisible) isVisible = IsMouseOutsideGameView();

            if (!isVisible && Cursor.visible)
            {
                // Move outside any Selectable UI
                MoveMousePosition();
                await Awaitable.NextFrameAsync();
                // After the Move command, necessary to wait until next frame to set Cursor.visible
            }

            Cursor.visible = isVisible;
            OnVisibilityChanged?.Invoke(isVisible);
        }

        private static void MoveMousePosition() =>
            UnityEngine.InputSystem.Mouse.current?.WarpCursorPosition(new Vector2(10f, Screen.height - 10f));

        private static bool IsMouseOutsideGameView()
        {
            var mouse = UnityEngine.InputSystem.Mouse.current;
            return mouse != null && !mouse.IsInsideGameView();
        }
    }
}
