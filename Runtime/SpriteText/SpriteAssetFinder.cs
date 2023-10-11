using TMPro;
using UnityEngine;

namespace ActionCode.InputSystem
{
    public static class SpriteAssetFinder
    {
        /// <summary>
        /// Updates the given input device if it's from <see cref="InputDeviceType.XboxSystem"/> 
        /// or <see cref="InputDeviceType.PlaystationSystem"/>.
        /// </summary>
        /// <param name="device"></param>
        public static void TryUpdateDeviceUsingSystem(ref InputDeviceType device)
        {
            if (device == InputDeviceType.XboxSystem)
            {
                device = GetDeviceFromAsset(
                    InputDeviceType.XboxSeriesX,
                    InputDeviceType.XboxOne,
                    InputDeviceType.Xbox360
                );
            }

            if (device == InputDeviceType.PlaystationSystem)
            {
                device = GetDeviceFromAsset(
                    InputDeviceType.Playstation5,
                    InputDeviceType.Playstation4,
                    InputDeviceType.Playstation3
                );
            }
        }

        /// <summary>
        /// Checks whether the project has a TMP Sprite Asset from any of the given device.
        /// </summary>
        /// <param name="devices">The devices type to check</param>
        /// <returns>Whether the project has a TMP Sprite Asset from any of the given device</returns>
        public static InputDeviceType GetDeviceFromAsset(params InputDeviceType[] devices)
        {
            foreach (var device in devices)
            {
                if (HasAssetFrom(device)) return device;
            }

            return InputDeviceType.None;
        }

        /// <summary>
        /// Checks whether the project has a TMP Sprite Asset from the given device.
        /// </summary>
        /// <param name="device">The device type to check.</param>
        /// <returns>Whether the project has a TMP Sprite Asset from the given device.</returns>
        public static bool HasAssetFrom(InputDeviceType device)
        {
            var asset = Resources.Load<TMP_SpriteAsset>(TMP_Settings.defaultSpriteAssetPath + device.ToString());
            return asset != null;
        }
    }
}