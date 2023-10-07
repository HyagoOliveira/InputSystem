using System;
using UnityEngine;
using ActionCode.Attributes;

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

        [ReadonlyIf(nameof(type), SpriteTagType.Animation)]
        [SerializeField, Tooltip("The Sprite Tag name.")]
        private string name;

        [ReadonlyIf(nameof(type), SpriteTagType.Name)]
        [SerializeField, Tooltip("The Sprite Tag animation initial index.")]
        private int initialIndex;

        [ReadonlyIf(nameof(type), SpriteTagType.Name)]
        [SerializeField, Tooltip("The Sprite Tag animation final index.")]
        private int finalIndex;

        [ReadonlyIf(nameof(type), SpriteTagType.Name)]
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