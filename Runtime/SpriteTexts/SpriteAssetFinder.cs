using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace ActionCode.InputSystem
{
    public static class SpriteAssetFinder
    {
        private static Dictionary<InputDeviceType, bool> DeviceAssets => lazyDeviceAssets.Value;

        private static readonly Lazy<Dictionary<InputDeviceType, bool>> lazyDeviceAssets = new(CreateLazyDeviceAssets);

        /// <summary>
        /// The default Sprite Asset folder path for UI Toolkit.
        /// </summary>
        public static string UITKDefaultSpriteAssetPath { get; internal set; }

        /// <summary>
        /// Updates the given input device using the closest TMP Sprite Asset available in the project.
        /// </summary>
        /// <param name="device"></param>
        public static void TryUpdateToAvailableDevice(ref InputDeviceType device)
        {
            var hasAsset = DeviceAssets[device];
            if (hasAsset) return;

            if (device == InputDeviceType.XboxSystem)
            {
                device = GetFirstAvailableFrom(
                    InputDeviceType.XboxSystem,
                    InputDeviceType.XboxSeriesX,
                    InputDeviceType.XboxOne,
                    InputDeviceType.Xbox360
                );
            }
            else if (device == InputDeviceType.PlaystationSystem)
            {
                device = GetFirstAvailableFrom(
                    InputDeviceType.PlaystationSystem,
                    InputDeviceType.Playstation5,
                    InputDeviceType.Playstation4,
                    InputDeviceType.Playstation3
                );
            }
        }

        private static Dictionary<InputDeviceType, bool> CreateLazyDeviceAssets()
        {
            var devices = Enum.GetValues(typeof(InputDeviceType)) as InputDeviceType[];
            var deviceAssets = new Dictionary<InputDeviceType, bool>(devices.Length);

            foreach (var device in devices)
            {
                var hasAsset = HasAssetFromTMP(device) || HasAssetFromUITK(device);
                deviceAssets.Add(device, hasAsset);
            }

            return deviceAssets;
        }

        private static bool HasAssetFromTMP(InputDeviceType device)
        {
            var path = TMP_Settings.defaultSpriteAssetPath + device.ToString();
            var asset = Resources.Load<TMP_SpriteAsset>(path);
            return asset != null;
        }

        private static bool HasAssetFromUITK(InputDeviceType device)
        {
            var path = UITKDefaultSpriteAssetPath + device.ToString();
            var asset = Resources.Load<SpriteAsset>(path);
            return asset != null;
        }

        private static InputDeviceType GetFirstAvailableFrom(params InputDeviceType[] devices)
        {
            foreach (var device in devices)
            {
                var hasAsset = DeviceAssets[device];
                if (hasAsset) return device;
            }

            return InputDeviceType.None;
        }
    }
}