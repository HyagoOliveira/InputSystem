using UnityEngine;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Disables the mouse cursor when a gamepad is used and enables it again when keyboard/mouse is used.
    /// <para>
    /// On Editor, if the mouse is outside the GameView, the cursor will be set to visible regardless 
    /// if using gamepad or not.
    /// </para>
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class DisableCursorListener : MonoBehaviour
    {
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
        public static void SetCursorVisibility(bool isVisible)
        {
            if (!isVisible) isVisible = IsMouseOutsideGameView();
            Cursor.visible = isVisible;
        }

        private static bool IsMouseOutsideGameView()
        {
            var mouse = UnityEngine.InputSystem.Mouse.current;
            return mouse != null && !mouse.IsInsideGameView();
        }
    }
}
