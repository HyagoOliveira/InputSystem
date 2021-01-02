using UnityEngine;
using System.Collections.Generic;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Generic <see cref="DeviceDisplayControl"/> container.
    /// </summary>
    [CreateAssetMenu(fileName = "DeviceSettings", menuName = "ActionCode/Input System/Device Display Settings", order = 110)]
    public sealed class DeviceDisplaySettings : ScriptableObject
    {
        [Tooltip("The device path.")]
        public string path;
        [Tooltip("The device display name.")]
        public string displayName;
        [Tooltip("The device main color.")]
        public Color mainColor = Color.white;
        [Tooltip("The device secondary color.")]
        public Color sideColor = Color.white;
        [Space]
        [Tooltip("All device display controls.")]
        public DeviceDisplayControl[] controls = new DeviceDisplayControl[0];

        /// <summary>
        /// Controls dictionary. The control path is the key.
        /// </summary>
        public Dictionary<string, DeviceDisplayControl> Controls { get; private set; }

        private void OnEnable()
        {
            LoadControlsDictionary();
        }

        /// <summary>
        /// Returns the control display name based on the given path.
        /// </summary>
        /// <param name="path">The control path.</param>
        /// <returns>The display name or an empty string if path is not found.</returns>
        public string GetDisplayName(string path)
        {
            var contains = Controls.ContainsKey(path);
            return contains ? Controls[path].name : string.Empty;
        }

        /// <summary>
        /// Returns the control sprite based on the given path.
        /// </summary>
        /// <param name="path">The control path.</param>
        /// <returns>The sprite or null if path is not found.</returns>
        public Sprite GetSprite(string path)
        {
            var contains = Controls.ContainsKey(path);
            return contains ? Controls[path].sprite : null;
        }

        private void LoadControlsDictionary()
        {
            Controls = new Dictionary<string, DeviceDisplayControl>(controls.Length);
            foreach (var control in controls)
            {
                var key = control.path;
                var canAdd = !Controls.ContainsKey(key);
                if (canAdd) Controls.Add(key, control);
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Bind Dual Motor Gamepad")]
        private void BindDualMotorGamepad()
        {
            const string path = "Packages/com.actioncode.input-system/Controls/DualMotorGamepad.json";
            BindControls(path);
        }

        private void BindControls(string path)
        {
            var jsonAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(path);
            JsonUtility.FromJsonOverwrite(jsonAsset.text, this);
        }
#endif
    }
}