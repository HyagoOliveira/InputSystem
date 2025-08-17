using UnityEngine.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Extension class for <see cref="InputAction"/> and <see cref="InputActionMap"/>.
    /// </summary>
    public static class InputActionExtension
    {
        /// <summary>
        /// Enables or disables the given <see cref="InputAction"/>.
        /// </summary>
        /// <param name="action">The action to enable or disable.</param>
        /// <param name="enabled">If true, the action will be enabled; otherwise, it will be disabled.</param>
    	public static void SetEnabled(this InputAction action, bool enabled)
        {
            if (enabled) action.Enable();
            else action.Disable();
        }

        /// <summary>
        /// Enables or disables the given <see cref="InputActionMap"/>.
        /// </summary>
        /// <param name="map">The map to enable or disable.</param>
        /// <param name="enabled">If true, the action will be enabled; otherwise, it will be disabled.</param>
    	public static void SetEnabled(this InputActionMap map, bool enabled)
        {
            if (enabled) map.Enable();
            else map.Disable();
        }
    }
}