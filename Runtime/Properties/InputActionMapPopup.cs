using System;
using UnityEngine.InputSystem;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Displays the action maps from a given <see cref="InputActionAsset"/> as a Popup field.
    /// </summary>
    [Serializable]
    public class InputActionMapPopup
    {
        public string mapName;
        public string assetName;

        /// <summary>
        /// Initializes the InputActionMapPopup.
        /// </summary>
        /// <param name="assetName">
        /// The name of the InputActionAsset. Put the name of your local InputActionAsset variable.
        /// <para><b>Tip</b>: use <c>nameof(assetName)</c>.</para>
        /// </param>
        /// <param name="mapName">The name of the InputActionMap present on your local InputActionAsset variable.</param>
        public InputActionMapPopup(string assetName, string mapName = "")
        {
            this.mapName = mapName;
            this.assetName = assetName;
        }
    }
}