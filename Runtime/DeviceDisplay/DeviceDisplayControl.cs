using UnityEngine;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Combines the device input path with its Sprite.
    /// </summary>
    [System.Serializable]
    public struct DeviceDisplayControl
    {
        [Tooltip("Input display name.")]
        public string name;
        [Tooltip("Input path.")]
        public string path;
        [Tooltip("Input Sprite.")]
        public Sprite sprite;

        /// <summary>
        /// Creates a Device Display Control with the given values.
        /// </summary>
        /// <param name="name">Input display name.</param>
        /// <param name="path">Input path.</param>
        /// <param name="sprite">Input Sprite.</param>
        public DeviceDisplayControl(string name, string path, Sprite sprite)
        {
            this.name = name;
            this.path = path;
            this.sprite = sprite;
        }

        /// <summary>
        /// Creates a Device Display Control with the given values.
        /// </summary>
        /// <param name="name">Input display name.</param>
        /// <param name="path">Input path.</param>
        public DeviceDisplayControl(string name, string path)
            : this(name, path, null) { }
    }
}