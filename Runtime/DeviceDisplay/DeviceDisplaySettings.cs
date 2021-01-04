using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using System.Collections.Generic;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Generic <see cref="DeviceDisplayControl"/> container.
    /// </summary>
    [CreateAssetMenu(fileName = "DeviceSettings", menuName = "ActionCode/Input System/Device Display Settings", order = 110)]
    public sealed class DeviceDisplaySettings : ScriptableObject
    {
        [Tooltip("The device type.")]
        public InputDeviceType type;
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
        /// <returns>A sprite or null if path is not found.</returns>
        public Sprite GetSprite(string path)
        {
            var contains = Controls.ContainsKey(path);
            return contains ? Controls[path].sprite : null;
        }

        /// <summary>
        /// Returns the control sprite based on the given action reference.
        /// </summary>
        /// <param name="actionReference">The input action reference.</param>
        /// <returns>A sprite or null if a reference is not found.</returns>
        public Sprite GetSprite(InputActionReference actionReference)
        {
            if (actionReference == null) return null;
            var bindings = actionReference.action.bindings.ToArray();
            foreach (var binding in bindings)
            {
                var sprite = GetSprite(binding.path);
                if (sprite) return sprite;
            }

            return null;
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

        [ContextMenu("Bind Keyboard And Mouse")]
        private void BindKeyboardAndMouse()
        {
            const string path = "Packages/com.actioncode.input-system/Controls/Keyboard&Mouse.json";
            BindControls(path);
        }

        private void BindControls(string path)
        {
            var sprites = controls.Select(display => display.sprite);
            var jsonAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(path);
            JsonUtility.FromJsonOverwrite(jsonAsset.text, this);

            var i = 0;
            foreach (Sprite sprite in sprites)
            {
                controls[i++].sprite = sprite;
            }
        }
#endif
    }
}