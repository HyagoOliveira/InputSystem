using TMPro;
using UnityEngine;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Abstract component to display Sprites using a local <see cref="TMP_Text"/> component.
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public abstract class AbstractSpriteText : MonoBehaviour
    {
        [SerializeField, Tooltip("The local TextMeshPro component.")]
        protected TMP_Text textMesh;
        [SerializeField, Tooltip("The input field that will be replaced by the TMP Sprite Tag.")]
        protected string inputField = "{input}";

        /// <summary>
        /// The original text from <see cref="textMesh"/> component.
        /// </summary>
        public string OriginalText { get; internal set; }

        protected virtual void Reset() => textMesh = GetComponent<TMP_Text>();
        protected virtual void Awake() => InitializeOriginalTextIfEmpty();
        protected virtual void OnEnable() => InputSystem.OnDeviceInputChanged += HandleDeviceInputChanged;
        protected virtual void OnDisable() => InputSystem.OnDeviceInputChanged -= HandleDeviceInputChanged;

        /// <summary>
        /// Updates the local <see cref="textMesh"/> using Sprites Tags according with the given device.
        /// </summary>
        /// <param name="device">The input device to update the Sprite Tags.</param>
        public void UpdateTextWithSpriteTag(InputDeviceType device)
        {
            SpriteAssetFinder.TryUpdateDeviceUsingSystem(ref device);
            textMesh.text = GetTextWithSpriteTag(device);
        }

        protected abstract string GetTextWithSpriteTag(InputDeviceType device);

        private void HandleDeviceInputChanged(InputDeviceType device) => UpdateTextWithSpriteTag(device);

        private void InitializeOriginalTextIfEmpty()
        {
            if (string.IsNullOrEmpty(OriginalText)) OriginalText = textMesh.text;
        }
    }
}