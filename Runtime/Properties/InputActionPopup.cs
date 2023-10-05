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
        /// <param name="assetName"><inheritdoc cref="InputActionMapPopup(string, string)"/></param>
        /// <param name="mapName">The name of the InputActionMap present on your local InputActionAsset variable.</param>
        /// <param name="actionName">The name of the action present on your local InputActionMap.</param>
        public InputActionPopup(string assetName, string mapName = "", string actionName = "")
            : base(assetName, mapName)
        {
            this.actionName = actionName;
        }

        /// <summary>
        /// Returns the path using the map and action name.
        /// </summary>
        /// <returns>Always a <c>string</c>.</returns>
        public string GetPath() => BuildPath(mapName, actionName);

        /// <summary>
        /// Builds an action path using mapName/actionName
        /// </summary>
        /// <param name="mapName">The map name to use.</param>
        /// <param name="actionName">The action name to use.</param>
        /// <returns>Always a <c>string</c>.</returns>
        public static string BuildPath(string mapName, string actionName) => $"{mapName}/{actionName}";
    }
}