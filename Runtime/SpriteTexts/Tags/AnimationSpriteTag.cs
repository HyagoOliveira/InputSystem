using UnityEngine;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Data container for an Animated Sprite Tag.
    /// </summary>
    [CreateAssetMenu(fileName = "AnimationSpriteTag", menuName = MENU_NAME + "Animation Sprite Tag", order = 111)]
    public sealed class AnimationSpriteTag : AbstractSpriteTag
    {
        [SerializeField, Tooltip("The animation speed.")]
        private int speed;
        [SerializeField, Tooltip("The animation indexes for keyboard and mouse.")]
        private AnimationSpriteTagIndex keyboard;
        [SerializeField, Tooltip("The animation indexes for any gamepad.")]
        private AnimationSpriteTagIndex gamepad;

        public override string GetTag(InputDeviceType device)
        {
            var indexes = GetIndexes(device);
            var assetName = device.ToString();
            return GetTagUsingAnimation(assetName, indexes.initialIndex, indexes.finalIndex, speed);
        }

        private AnimationSpriteTagIndex GetIndexes(InputDeviceType device)
        {
            var isKeyboard = device == InputDeviceType.KeyboardAndMouse;
            return isKeyboard ? keyboard : gamepad;
        }

        private static string GetTagUsingAnimation(
            string assetName,
            int intialIndex,
            int finalIndex,
            int speed
        ) => $"<sprite=\"{assetName}\" anim=\"{intialIndex}, {finalIndex}, {speed}\">";
    }
}