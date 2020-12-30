using UnityEngine.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Displays the actions from a given <see cref="InputActionMap"/> as a Popup field.
    /// </summary>
    [System.Serializable]
    public sealed class InputActionPopup : InputActionMapPopup
    {
        public string actionName;

        /// <summary>
        /// Initializes the InputActionPopup.
        /// </summary>
        /// <param name="assetName">The name of the InputActionAsset. Put the name of your local InputActionAsset variable.</param>
        /// <param name="mapName">The name of the InputActionMap present on your local InputActionAsset variable.</param>
        /// <param name="actionName">The name of the action present on your local InputActionMap.</param>
        public InputActionPopup(string assetName, string mapName, string actionName)
            : base(assetName, mapName)
        {
            this.actionName = actionName;
        }

        /// <summary>
        /// Returns the input action from the given map.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public InputAction GetAction(InputActionMap map)
        {
            return map.FindAction(actionName);
        }
    }
}