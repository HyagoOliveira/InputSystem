using System;
using UnityEngine;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Holds data for a Sprite Tag.
    /// </summary>
    [Serializable]
    public struct SpriteTag
    {
        [SerializeField, Tooltip("The Sprite Tag type.")]
        private SpriteTagType type;

        [SerializeField, Tooltip("The Sprite Tag name.")]
        private string name;

        [SerializeField, Tooltip("The Sprite Tag animation initial index.")]
        private int initialIndex;
        [SerializeField, Tooltip("The Sprite Tag animation final index.")]
        private int finalIndex;
        [SerializeField, Tooltip("The Sprite Tag animation speed.")]
        private int speed;

        public readonly string GetSpriteTag(string assetName) => type switch
        {
            SpriteTagType.Name => GetSpriteTagUsingName(assetName, name),
            SpriteTagType.Animation => GetSpriteTagUsingAnimation(assetName, initialIndex, finalIndex, speed),
            _ => string.Empty
        };

        public static string GetSpriteTagUsingName(string assetName, string spriteName) =>
            $"<sprite=\"{assetName}\" name=\"{spriteName}\">";

        public static string GetSpriteTagUsingAnimation(
            string assetName,
            int intialIndex,
            int finalIndex,
            int speed
        ) => $"<sprite=\"{assetName}\" anim=\"{intialIndex}, {finalIndex}, {speed}\">";
    }
}