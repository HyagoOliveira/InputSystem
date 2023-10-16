using UnityEngine;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Data container for a custom Sprite Tag.
    /// </summary>
    [CreateAssetMenu(fileName = "CustomSpriteTag", menuName = MENU_NAME + "Custom Sprite Tag", order = 112)]
    public sealed class CustomSpriteTag : AbstractSpriteTag
    {
        [SerializeField, Tooltip("The animation name for keyboard and mouse.")]
        private string keyboard;
        [SerializeField, Tooltip("The animation name for any gamepad.")]
        private string gamepad;

        public override string GetTag(InputDeviceType device)
        {
            var spriteName = GetIndexes(device);
            var assetName = device.ToString();
            return GetTagUsingName(assetName, spriteName);
        }

        private string GetIndexes(InputDeviceType device)
        {
            var isKeyboard = device == InputDeviceType.KeyboardAndMouse;
            return isKeyboard ? keyboard : gamepad;
        }
    }
}