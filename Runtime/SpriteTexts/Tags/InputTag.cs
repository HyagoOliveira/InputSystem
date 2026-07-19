using System;
using UnityEngine;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Serializable class holding a Tag and a Navigation UI instance. Use to both show and use Tag actions.
    /// </summary>
    /// <remarks>
    /// If you don't have or need a Navigation UI instance, use any <see cref="AbstractSpriteTag"/> implementation instead.
    /// </remarks>
    [Serializable]
    public sealed class InputTag : IDisposable
    {
        [SerializeField, Tooltip("The Input Sprite Tag asset.")]
        private AbstractSpriteTag tag;
        [SerializeField, Tooltip("The navigation instance showing the input on the Scene.")]
        private GameObject navigation;

        /// <summary>
        /// The Input Sprite Tag asset.
        /// </summary>
        public AbstractSpriteTag Tag => tag;

        public void Initialize() => tag.Initialize();
        public void Dispose() => tag.Dispose();

        public void SetActive(bool isActive)
        {
            navigation.SetActive(isActive);
            Tag.Action.SetEnabled(isActive);
        }
    }
}