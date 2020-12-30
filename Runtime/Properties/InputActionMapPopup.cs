using UnityEngine.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Displays the action maps from a given <see cref="InputActionAsset"/> as a Popup field.
    /// </summary>
    [System.Serializable]
    public class InputActionMapPopup
    {
        public string mapName;
        public string assetName;

        /// <summary>
        /// Initializes the InputActionMapPopup.
        /// </summary>
        /// <param name="assetName">The name of the InputActionAsset. Put the name of your local InputActionAsset variable.</param>
        /// <param name="mapName">The name of the InputActionMap present on your local InputActionAsset variable.</param>
        public InputActionMapPopup(string assetName, string mapName = "")
        {
            this.mapName = mapName;
            this.assetName = assetName;
        }

        /// <summary>
        /// Finds an ActionMap inside the given actionAsset and returns it.
        /// </summary>
        /// <param name="actionAsset">InputActionAsset instance.</param>
        /// <param name="name">Name of the ActionMap.</param>
        /// <returns>An InputActionMap instance if found. Null otherwise.</returns>
        public InputActionMap FindActionMap(InputActionAsset actionAsset, string name)
        {
            InputActionMap actionMap = actionAsset.FindActionMap(name);
            if (actionMap == null)
            {
                UnityEngine.Debug.LogErrorFormat("Cannot find action map '{0}' in actions '{1}'", name, actionAsset);
                return null;
            }

            mapName = name;
            return actionMap;
        }
    }
}