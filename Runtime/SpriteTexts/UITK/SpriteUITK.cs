#if UI_TOOLKIT
using UnityEngine;
using ActionCode.SerializedDictionaries;
using UnityEngine.UIElements;
using System.Linq;
using System.Collections.Generic;

namespace ActionCode.InputSystem
{
    /// <summary>
    /// Displays multiple Sprite Tags into Labels inside a local <see cref="UIDocument"/> by replacing all 
    /// occurrences of the keys from <see cref="inputSpriteTags"/> dictionary by its Sprite Tag.
    /// </summary>
    /// <remarks>
    /// Place this component into the same GameObject containing a UIDocument component and set any label from the document as:
    /// 
    /// <code>Press {jump} to jump.</code>
    /// 
    /// Next, set the <see cref="inputSpriteTags"/>, adding an entry with <c>{jump}</c> as key and an appropriated Sprite Tag asset.
    /// At runtime, all occurrences of <c>{jump}</c> will be replaced by this Sprite Tag.
    /// <br/>
    /// You can create a Sprite Tag asset using the creating menu, ActionCode/Input System/.
    /// 
    /// <para> 
    /// <b>Tip</b>: if your project uses the Localization System provided by Unity, also attach 
    /// the <see cref="LocalizedSpriteTMP"/> component in the same GameObject.
    /// </para>
    /// </remarks>
    [RequireComponent(typeof(UIDocument))]
    public sealed class SpriteUITK : MonoBehaviour
    {
        [SerializeField, Tooltip("The local UI Document component.")]
        private UIDocument document;
        [SerializeField, Tooltip("The input sprite tags dictionary.")]
        private SerializedDictionary<string, AbstractSpriteTag> inputSpriteTags = new();
        [SerializeField]
        private string className = "navegation-label";

        public VisualElement Root => document.rootVisualElement;
        public Dictionary<Label, string> Labels { get; private set; }
        public string SpriteAssetPath => document.panelSettings.textSettings.defaultSpriteAssetPath;

        private void Reset() => document = GetComponent<UIDocument>();
        private void Awake() => SpriteAssetFinder.UITKDefaultSpriteAssetPath = SpriteAssetPath;

        private void OnEnable()
        {
            FindOriginalSourceTexts();
            InputSystem.OnDeviceInputChanged += HandleDeviceInputChanged;
        }

        private void OnDisable() => InputSystem.OnDeviceInputChanged -= HandleDeviceInputChanged;

        public async void UpdateTextWithSpriteTags(InputDeviceType device)
        {
            SpriteAssetFinder.TryUpdateToAvailableDevice(ref device);

            await Awaitable.NextFrameAsync();

            foreach (var (label, sourceText) in Labels)
            {
                label.text = GetTextWithSpriteTags(sourceText, device);
            }
        }

        private string GetTextWithSpriteTags(string text, InputDeviceType device)
        {
            foreach (var (tag, sprite) in inputSpriteTags)
            {
                text = text.Replace(tag, sprite.GetTag(device));
            }

            return text;
        }

        private void FindOriginalSourceTexts()
        {
            Labels = Root.Query<Label>(className: className)
                .ToList()
                .ToDictionary(label => label, label => label.text);
        }

        private void HandleDeviceInputChanged(InputDeviceType device) => UpdateTextWithSpriteTags(device);
    }
}
#endif