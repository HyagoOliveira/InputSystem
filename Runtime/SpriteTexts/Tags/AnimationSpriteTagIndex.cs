using UnityEngine;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Data container for an Animated Sprite Tag indexes.
    /// </summary>
    [System.Serializable]
    public struct AnimationSpriteTagIndex
    {
        [Tooltip("The animation initial index.")]
        public int initialIndex;
        [Tooltip("The animation final index.")]
        public int finalIndex;
    }
}