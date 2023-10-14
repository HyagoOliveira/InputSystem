using UnityEngine;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Abstract data container for a Sprite Tag.
    /// </summary>
    public abstract class AbstractSpriteTag : ScriptableObject
    {
        protected const string MENU_NAME = "ActionCode/Input System/";

        /// <summary>
        /// Returns the Sprite Tag for the given device.
        /// </summary>
        /// <param name="device">The device used to find the appropriate Sprite Tag.</param>
        /// <returns>A string containing a TMP Sprite Tag</returns>
        public abstract string GetTag(InputDeviceType device);
    }
}