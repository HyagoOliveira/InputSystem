using UnityEngine;
using System.Collections.Generic;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Container for <see cref="DeviceDisplaySettings"/>.
    /// </summary>
    [CreateAssetMenu(fileName = "DisplaySet", menuName = "ActionCode/Input System/Device Display Set", order = 110)]
    public sealed class DeviceDisplaySet : ScriptableObject
    {
        /// <summary>
        /// Device Display Settings group.
        /// </summary>
        public DeviceDisplaySettings[] settings = new DeviceDisplaySettings[0];

        /// <summary>
        /// Device Display Settings dictionary. The device path is the key.
        /// </summary>
        public Dictionary<string, DeviceDisplaySettings> Settings { get; private set; }

        private void OnEnable()
        {
            LoadSettingsDictionary();
        }

        /// <summary>
        /// Returns the device settings based on the given path.
        /// </summary>
        /// <param name="path">The device path.</param>
        /// <returns>The device settings or null if path is not found.</returns>
        public DeviceDisplaySettings GetSettings(string path)
        {
            var contains = Settings.ContainsKey(path);
            return contains ? Settings[path] : null;
        }

        private void LoadSettingsDictionary()
        {
            Settings = new Dictionary<string, DeviceDisplaySettings>(settings.Length);
            foreach (var setting in settings)
            {
                var key = setting.path;
                var canAdd = !Settings.ContainsKey(key);
                if (canAdd) Settings.Add(key, setting);
            }
        }
    }
}