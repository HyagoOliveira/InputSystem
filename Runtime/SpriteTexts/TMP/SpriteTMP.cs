using TMPro;
using UnityEngine;
using ActionCode.SerializedDictionaries;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Displays multiple Sprite Tags into the local <see cref="TMP_Text"/> component by replacing all 
    /// occurrences of the keys from <see cref="inputSpriteTags"/> dictionary by its Sprite Tag.
    /// </summary>
    /// <remarks>
    /// Place this component into the same GameObject containing a TextMeshPro component and set its text field to:
    /// 
    /// <code>Press {jump} to jump.</code>
    /// 
    /// Next, set the <see cref="inputSpriteTags"/>, adding an entry with <c>{jump}</c> as key and an appropriated Sprite Tag asset.
    /// At runtime, all occurrences of <c>{jump}</c> will be replaced by this Sprite Tag.
    /// <br/>
    /// You can create any Sprite Tag asset using the creating menu, ActionCode/Input System/.
    /// 
    /// <para> 
    /// <b>Tip</b>: if your project uses the Localization System provided by Unity, also attach 
    /// the LocalizedSpriteTMP component in the same GameObject.
    /// </para>
    /// </remarks>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TMP_Text))]
    public sealed class SpriteTMP : MonoBehaviour
    {
        [SerializeField, Tooltip("The local TextMeshPro component.")]
        private TMP_Text textMesh;
        [SerializeField, Tooltip("The input sprite tags dictionary.")]
        private SerializedDictionary<string, AbstractSpriteTag> inputSpriteTags = new();

        /// <summary>
        /// The current source text without any Sprite Tag.
        /// </summary>
        public string SourceText { get; private set; }

        private void Reset() => textMesh = GetComponent<TMP_Text>();
        private void Awake() => SourceText = textMesh.text;
        private void OnEnable() => InputSystem.OnDeviceInputChanged += HandleDeviceInputChanged;
        private void OnDisable() => InputSystem.OnDeviceInputChanged -= HandleDeviceInputChanged;

        /// <summary>
        /// Sets the current source text.
        /// </summary>
        /// <param name="text">A text without any Sprite Tag.</param>
        public void SetSourceText(string text)
        {
            SourceText = text;
            if (InputSystem.LastDeviceType != InputDeviceType.None)
                UpdateTextWithSpriteTags(InputSystem.LastDeviceType);
        }

        private void UpdateTextWithSpriteTags(InputDeviceType device)
        {
            SpriteAssetFinder.TryUpdateToAvailableDevice(ref device);
            textMesh.text = GetTextWithSpriteTags(device);
        }

        private string GetTextWithSpriteTags(InputDeviceType device)
        {
            var text = SourceText;

            foreach (var (tag, sprite) in inputSpriteTags)
            {
                text = text.Replace(tag, sprite.GetTag(device));
            }

            return text;
        }

        private void HandleDeviceInputChanged(InputDeviceType device) => UpdateTextWithSpriteTags(device);
    }
}